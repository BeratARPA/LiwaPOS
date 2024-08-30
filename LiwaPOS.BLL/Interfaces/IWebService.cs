namespace LiwaPOS.BLL.Interfaces
{
    public interface IWebService
    {
        void Navigate(string url);
        void Reload();
        void GoBack();
        void GoForward();
        void ExecuteScript(string script);
        void OpenWebsiteOnWindow(string title, bool useBorder, bool useFullscreen, int width, int height);
    }
}
