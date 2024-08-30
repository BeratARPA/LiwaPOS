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

        public async void Navigate(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                _webView.Source = uri;
            }
            else
            {
                await LoggingService.LogErrorAsync("Invalid URL format", typeof(WebService).Name, url, new ArgumentException());
            }
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

        public void ExecuteScript(string script)
        {
            _webView.ExecuteScriptAsync(script);
        }

        public void OpenWebsiteOnWindow(string title, bool useBorder, bool useFullscreen, int width, int height)
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
