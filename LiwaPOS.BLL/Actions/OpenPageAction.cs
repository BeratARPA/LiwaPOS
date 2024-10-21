using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class OpenPageAction : IAction
    {
        private readonly UserManager _userManager;
        private readonly INavigatorService _navigatorService;
        private readonly IApplicationStateService _applicationStateService;

        public OpenPageAction(UserManager userManager, INavigatorService navigatorService, IApplicationStateService applicationStateService)
        {
            _userManager = userManager;
            _navigatorService = navigatorService;
            _applicationStateService = applicationStateService;
        }

        public async Task Execute(string properties)
        {
            var openPageProperties = JsonHelper.Deserialize<OpenPageDTO>(properties);
            if (openPageProperties == null)
                return;

            if (_applicationStateService.CurrentLoggedInUser == null)
                return;

            _navigatorService.Navigate(openPageProperties.Name);
        }
    }
}
