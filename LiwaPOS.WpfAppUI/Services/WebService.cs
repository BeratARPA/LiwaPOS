using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Services;
using LiwaPOS.WpfAppUI.Views;
using Microsoft.Web.WebView2.Wpf;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Services
{
    public class WebService : IWebService
    {
        private WebView2 _webView;

        public async Task NavigateURL(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                _webView.Source = uri;
            }
            else
            {
                await LoggingService.LogErrorAsync("Invalid URL format", typeof(WebService).Name, url, new ArgumentException());
            }
        }

        public async void NavigateHTMLContent(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent))
                return;

            await _webView.EnsureCoreWebView2Async();
            _webView.NavigateToString(htmlContent);
        }

        public void Reload()
        {
            _webView.Reload();
        }

        public void GoBack()
        {
            if (_webView.CanGoBack)
            {
                _webView.GoBack();
            }
        }

        public void GoForward()
        {
            if (_webView.CanGoForward)
            {
                _webView.GoForward();
            }
        }

        public async Task ExecuteScript(string script)
        {
            if (string.IsNullOrEmpty(script))
                return;

            await _webView.ExecuteScriptAsync(script);
        }

        public void OpenWebsiteOnWindow(string title = "Web", bool useBorder = true, bool useFullscreen = false, int width = 400, int height = 400)
        {
            WebViewWindow webViewWindow = new WebViewWindow();
            webViewWindow.Title = title;
            webViewWindow.WindowStyle = useBorder ? WindowStyle.SingleBorderWindow : WindowStyle.None;
            webViewWindow.Width = width;
            webViewWindow.Height = height;
            webViewWindow.WindowState = useFullscreen ? WindowState.Maximized : WindowState.Normal;
            webViewWindow.WindowStyle = useFullscreen ? WindowStyle.SingleBorderWindow : webViewWindow.WindowStyle;
            _webView = webViewWindow.webView;
            webViewWindow.Show();
        }
    }
}
