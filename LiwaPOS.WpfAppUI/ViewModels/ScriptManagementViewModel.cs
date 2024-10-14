using LiwaPOS.WpfAppUI.Commands;
using Microsoft.Web.WebView2.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ScriptManagementViewModel : INotifyPropertyChanged
    {
        private string _scriptName;
        private string _monacoEditorSource;
        private CoreWebView2 _webView;

        public string ScriptName
        {
            get => _scriptName;
            set
            {
                _scriptName = value;
                OnPropertyChanged(nameof(ScriptName));
            }
        }

        public string MonacoEditorSource
        {
            get => _monacoEditorSource;
            set
            {
                _monacoEditorSource = value;
                OnPropertyChanged(nameof(MonacoEditorSource));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand RunCommand { get; }

        public ScriptManagementViewModel()
        {
            SaveCommand = new RelayCommand(SaveScript);
            RunCommand = new RelayCommand(RunScript);

            MonacoEditorSource = @"\Resources\monacoeditor.html";  // Yerel dosya yolunu veya URI'yi belirtin
        }

        private async void SaveScript(object obj)
        {
            if (_webView != null)
            {
                // Monaco Editor'deki kodu alma
                string editorContent = await _webView.ExecuteScriptAsync("getEditorContent();");
            }
        }

        private async void RunScript(object obj)
        {
            if (_webView != null)
            {
                // Monaco Editor'deki kodu alma ve çalıştırma simülasyonu
                string editorContent = await _webView.ExecuteScriptAsync("getEditorContent();");

                MessageBox.Show($"Çalıştırılan Kod:\n{editorContent}");
            }
        }

        public void SetWebView(CoreWebView2 webView)
        {
            _webView = webView;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
