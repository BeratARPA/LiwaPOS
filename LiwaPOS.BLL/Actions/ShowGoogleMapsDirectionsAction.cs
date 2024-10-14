using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class ShowGoogleMapsDirectionsAction : IAction
    {
        private readonly IWebService _webService;
        private readonly IGoogleMapService _googleMapService;

        public ShowGoogleMapsDirectionsAction(IWebService webService, IGoogleMapService googleMapService)
        {
            _webService = webService;
            _googleMapService = googleMapService;
        }

        public async Task Execute(string properties)
        {
            var showGoogleMapsDirectionProperties = JsonHelper.Deserialize<ShowGoogleMapsDirectionDTO>(properties);
            if (showGoogleMapsDirectionProperties == null)
                return;

            string content = await _googleMapService.GetDirectionAsync(showGoogleMapsDirectionProperties);

            _webService.OpenWebsiteOnWindow();
            _webService.NavigateHTMLContent(content);
        }
    }
}
