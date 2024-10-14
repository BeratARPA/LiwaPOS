namespace LiwaPOS.BLL.Interfaces
{
    public interface IWebService
    {
        Task NavigateURL(string url);
        void NavigateHTMLContent(string htmlContent);
        void Reload();
        void GoBack();
        void GoForward();
        Task ExecuteScript(string script);
        void OpenWebsiteOnWindow(string title = "Web", bool useBorder = true, bool useFullscreen = false, int width = 400, int height = 400);
    }
}
