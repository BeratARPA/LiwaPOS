using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class OpenWebsiteOnWindowAction : IAction
    {
        private readonly IWebService _webService;

        public OpenWebsiteOnWindowAction(IWebService webService)
        {
            _webService = webService;
        }

        public async Task<object> Execute(string properties)
        {
            var openWebsiteOnWindowProperties = JsonHelper.Deserialize<OpenWebsiteOnWindowDTO>(properties);
            if (openWebsiteOnWindowProperties == null)
                return false;

            string protocol = (bool)openWebsiteOnWindowProperties.UseHttps ? "https://{0}" : "http://{0}";
            string url = string.Format(protocol, openWebsiteOnWindowProperties.URL);

            _webService.OpenWebsiteOnWindow(openWebsiteOnWindowProperties.Title, (bool)openWebsiteOnWindowProperties.UseBorder, (bool)openWebsiteOnWindowProperties.UseFullscreen, (int)openWebsiteOnWindowProperties.Width, (int)openWebsiteOnWindowProperties.Height);
            await _webService.NavigateURL(url);

            return true;
        }
    }
}
