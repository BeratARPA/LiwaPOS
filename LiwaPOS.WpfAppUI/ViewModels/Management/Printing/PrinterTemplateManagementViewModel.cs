using DevExpress.Xpf.Core;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.ValueChangeSystem;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using Microsoft.Web.WebView2.Wpf;
using PrintHTML.Core.Helpers;
using PrintHTML.Core.Services;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Printing
{
    public class PrinterTemplateManagementViewModel : ViewModelBase
    {
        private readonly IDynamicValueResolver _dynamicValueResolver;
        private readonly IPrinterTemplateService _printerTemplateService;
        private readonly ICustomNotificationService _customNotificationService;
        private readonly PrinterService _printerService = new PrinterService();
        private string _templateName;
        private string _template;
        private int _templateId;
        private string _monacoEditorSource;
        private FlowDocument _document;
        private WebView2 _webView;

        public int TemplateId
        {
            get => _templateId;
            set
            {
                _templateId = value;
                OnPropertyChanged();
            }
        }

        public string TemplateName
        {
            get => _templateName;
            set
            {
                _templateName = value;
                OnPropertyChanged();
            }
        }

        public string Template
        {
            get => _template;
            set
            {
                _template = value;
                OnPropertyChanged();
            }
        }

        public FlowDocument Document
        {
            get => _document;
            set
            {
                _document = value;
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
        public ICommand PreviewCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand TemplateHelpCommand { get; }

        public PrinterTemplateManagementViewModel(IDynamicValueResolver dynamicValueResolver, IPrinterTemplateService printerTemplateService, ICustomNotificationService customNotificationService)
        {
            _dynamicValueResolver = dynamicValueResolver;
            _printerTemplateService = printerTemplateService;
            _customNotificationService = customNotificationService;

            SaveCommand = new AsyncRelayCommand(SaveTemplate);
            CloseCommand = new AsyncRelayCommand(ClosePage);
            PreviewCommand = new AsyncRelayCommand(PreviewTemplate);
            PrintCommand = new AsyncRelayCommand(PrintTemplate);
            TemplateHelpCommand = new AsyncRelayCommand(TemplateHelp);

            // HTML file path for Monaco Editor
            string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(assemblyPath, "Resources", "monacoeditorprintertemplate.html");

            // Dosya yolunu URI formatına çevirirken özel karakterleri uygun şekilde kodlama
            MonacoEditorSource = new Uri(filePath).AbsoluteUri;
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is PrinterTemplateDTO printerTemplate)
            {
                TemplateId = printerTemplate.Id;
                TemplateName = printerTemplate.Name;
                Template = printerTemplate.Template;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("PrinterTemplates");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
            {
                (
                nameof(TemplateName),
                async () => !string.IsNullOrEmpty(TemplateName),
                string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(TemplateName))
                ),
                (
                nameof(TemplateName),
                async () => !(await _printerTemplateService.GetAllPrinterTemplatesAsNoTrackingAsync(x => x.Name == TemplateName&& x.Id != TemplateId)).Any(),
                string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(TemplateName))
                )
            };
        }

        private async Task SaveTemplate(object obj)
        {
            if (_webView == null)
                return;

            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var content = await GetEditorContent();
            if (string.IsNullOrEmpty(content))
                return;

            var existingPrinterTemplate = await _printerTemplateService.GetPrinterTemplateByIdAsNoTrackingAsync(TemplateId);
            if (existingPrinterTemplate != null)
            {
                existingPrinterTemplate.Name = TemplateName;
                existingPrinterTemplate.Template = content;

                await _printerTemplateService.UpdatePrinterTemplateAsync(existingPrinterTemplate);
            }
            else
            {
                var printerTemplate = new PrinterTemplateDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = TemplateName,
                    Template = content
                };

                await _printerTemplateService.AddPrinterTemplateAsync(printerTemplate);
            }

            GlobalVariables.Navigator.Navigate("PrinterTemplates");
        }

        private async Task PreviewTemplate(object obj)
        {
            var content = await GetEditorContent();
            if (string.IsNullOrEmpty(content))
                return;

            var context = new ValueContext(null, null);
            var result = _dynamicValueResolver.ResolveExpression(content, context);

            Document = _printerService.GeneratePreview(result);
        }

        private async Task PrintTemplate(object obj)
        {
            var content = await GetEditorContent();
            if (string.IsNullOrEmpty(content))
                return;

            var context = new ValueContext(null, null);
            var result = _dynamicValueResolver.ResolveExpression(content, context);

            AsyncPrintTask.Exec(true, () => _printerService.DoPrint(result, PrinterHelper.GetDefaultPrinter()));
        }

        private async Task TemplateHelp(object obj)
        {
            string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(assemblyPath, "Resources", "TokenHelp.xaml");
            if (!FileExtension.Exists(filePath))
            {
                Document = new FlowDocument();
                return;
            }
            
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                FlowDocument tokenHelpDocument = XamlReader.Load(fileStream) as FlowDocument ?? new FlowDocument();
                Document = tokenHelpDocument;
            }
        }

        private async Task<string> GetEditorContent()
        {
            if (_webView == null)
                return "";

            string editorContent = await _webView.ExecuteScriptAsync("window.editor.getValue();");
            // Gelen string JSON formatında olabilir, bunu düzenle:
            editorContent = editorContent.Trim('"').Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            // Unicode kaçış karakterlerini çözümlemek için bir Regex kullan
            editorContent = Regex.Unescape(editorContent);
            // Kaçış karakterlerini kaldır (örn. \\u003C yerine < koy)
            editorContent = Regex.Replace(editorContent, @"\\u([0-9A-Fa-f]{4})", m => ((char)int.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber)).ToString());

            return editorContent;
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
                        await _webView.ExecuteScriptAsync($"window.editor.setValue(`{Template}`);");
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
