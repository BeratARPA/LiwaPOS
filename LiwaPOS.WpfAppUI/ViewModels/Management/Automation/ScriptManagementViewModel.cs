using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Services;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class ScriptManagementViewModel : ViewModelBase
    {
        private readonly IScriptService _scriptService;
        private readonly ICustomNotificationService _customNotificationService;
        private readonly JavaScriptEngineService _javaScriptEngineService;
        private string _scriptName;
        private string _script;
        private int _scriptId;
        private string _monacoEditorSource;
        private WebView2 _webView;

        public int ScriptId
        {
            get => _scriptId;
            set
            {
                _scriptId = value;
                OnPropertyChanged();
            }
        }

        public string ScriptName
        {
            get => _scriptName;
            set
            {
                _scriptName = value;
                OnPropertyChanged();
            }
        }

        public string Script
        {
            get => _script;
            set
            {
                _script = value;
                OnPropertyChanged();
            }
        }

        public string MonacoEditorSource
        {
            get => _monacoEditorSource;
            set
            {
                _monacoEditorSource = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand RunCommand { get; }

        public ScriptManagementViewModel(IScriptService scriptService, JavaScriptEngineService javaScriptEngineService, ICustomNotificationService customNotificationService)
        {
            _scriptService = scriptService;
            _javaScriptEngineService = javaScriptEngineService;
            _customNotificationService = customNotificationService;

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
            RunCommand = new AsyncRelayCommand(RunScript);

            // HTML file path for Monaco Editor
            string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(assemblyPath, "Resources", "monacoeditor.html");

            // Dosya yolunu URI formatına çevirirken özel karakterleri uygun şekilde kodlama
            MonacoEditorSource = new Uri(filePath).AbsoluteUri;
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is ScriptDTO script)
            {
                ScriptId = script.Id;
                ScriptName = script.Name;
                Script = script.Code;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("Scripts");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(ScriptName),
            async () => !string.IsNullOrEmpty(ScriptName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(ScriptName))
        ),
        (
            nameof(ScriptName),
            async () => !(await _scriptService.GetAllScriptsAsNoTrackingAsync(x => x.Name == ScriptName)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(ScriptName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            if (_webView == null)
                return;

            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            string editorContent = await _webView.ExecuteScriptAsync("window.editor.getValue();");
            // Gelen string JSON formatında olabilir, bunu düzenle:
            editorContent = editorContent.Trim('"').Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            // Unicode kaçış karakterlerini çözümlemek için bir Regex kullan
            editorContent = Regex.Unescape(editorContent);
            // Kaçış karakterlerini kaldır (örn. \\u003C yerine < koy)
            editorContent = Regex.Replace(editorContent, @"\\u([0-9A-Fa-f]{4})", m => ((char)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString());
            if (string.IsNullOrEmpty(editorContent))
                return;

            var existingScript = await _scriptService.GetScriptByIdAsNoTrackingAsync(ScriptId);
            if (existingScript != null)
            {
                existingScript.Name = ScriptName;
                existingScript.Code = editorContent;

                await _scriptService.UpdateScriptAsync(existingScript);
            }
            else
            {
                var script = new ScriptDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = ScriptName,
                    Code = editorContent
                };

                await _scriptService.AddScriptAsync(script);
            }

            GlobalVariables.Navigator.Navigate("Scripts");
        }

        private async Task RunScript(object obj)
        {
            if (_webView == null)
                return;

            string editorContent = await _webView.ExecuteScriptAsync("window.editor.getValue();");
            // Gelen string JSON formatında olabilir, bunu düzenle:
            editorContent = editorContent.Trim('"').Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            // Unicode kaçış karakterlerini çözümlemek için bir Regex kullan
            editorContent = Regex.Unescape(editorContent);
            // Kaçış karakterlerini kaldır (örn. \\u003C yerine < koy)
            editorContent = Regex.Replace(editorContent, @"\\u([0-9A-Fa-f]{4})", m => ((char)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString());

            var result = _javaScriptEngineService.ExecuteJavaScript(editorContent);
        }

        public void SetWebView(WebView2 webView)
        {
            _webView = webView;

            // Initialize WebView2 control
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            if (_webView != null)
            {
                // Initialize WebView2 and load the Monaco editor
                await _webView.EnsureCoreWebView2Async();
                _webView.CoreWebView2.Navigate(MonacoEditorSource);

                _webView.CoreWebView2.NavigationCompleted += async (sender, e) =>
                {
                    if (e.IsSuccess)
                    {
                        await _webView.ExecuteScriptAsync($"window.editor.setValue(`{Script}`);");
                    }
                    else
                    {
                        MessageBox.Show("WebView2 sayfası yüklenirken bir hata oluştu.");
                    }
                };
            }
        }
    }
}
