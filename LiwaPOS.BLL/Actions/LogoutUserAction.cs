using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;

namespace LiwaPOS.BLL.Actions
{
    public class LogoutUserAction : IAction
    {
        private readonly INavigatorService _navigatorService;
        private readonly IApplicationStateService _applicationStateService;

        public LogoutUserAction(UserManager userManager, INavigatorService navigatorService, IApplicationStateService applicationStateService)
        {
            _navigatorService = navigatorService;
            _applicationStateService = applicationStateService;
        }

        public async Task Execute(string properties)
        {
            _applicationStateService.CurrentLoggedInUser = null;
            _applicationStateService.SetTextBlockUsername();

            _applicationStateService.SetGridBottomBarVisibility(VisibilityState.Collapsed);
            _navigatorService.Navigate("Login");
        }
    }
}
