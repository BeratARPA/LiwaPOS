using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class OpenPageAction : IAction
    {      
        private readonly INavigatorService _navigatorService;
        private readonly IApplicationStateService _applicationStateService;

        public OpenPageAction(INavigatorService navigatorService, IApplicationStateService applicationStateService)
        {        
            _navigatorService = navigatorService;
            _applicationStateService = applicationStateService;
        }

        public async Task<object> Execute(string properties)
        {
            var openPageProperties = JsonHelper.Deserialize<OpenPageDTO>(properties);
            if (openPageProperties == null)
                return false;

            if (_applicationStateService.CurrentLoggedInUser == null)
                return false;

            _navigatorService.Navigate(openPageProperties.Name);

            return true;
        }
    }
}
