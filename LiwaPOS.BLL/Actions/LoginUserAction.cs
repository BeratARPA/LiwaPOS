using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class LoginUserAction : IAction
    {
        private readonly UserManager _userManager;
        private readonly INavigatorService _navigatorService;
        private readonly IApplicationStateService _applicationStateService;

        public LoginUserAction(UserManager userManager, INavigatorService navigatorService, IApplicationStateService applicationStateService)
        {
            _userManager = userManager;
            _navigatorService = navigatorService;
            _applicationStateService = applicationStateService;
        }

        public async Task Execute(string properties)
        {
            var loginUserProperties = JsonHelper.Deserialize<LoginUserDTO>(properties);
            if (loginUserProperties == null)
                return;

            bool isSuccessful = await _userManager.Login(loginUserProperties.PinCode);
            if (isSuccessful)
            {
                _applicationStateService.CurrentLoggedInUser = await _userManager.GetUserByPinCode(loginUserProperties.PinCode);
                _applicationStateService.SetTextBlockUsername();

                _applicationStateService.SetGridBottomBarVisibility(VisibilityState.Visible);
                _navigatorService.Navigate("Navigation");
            }
        }
    }
}
