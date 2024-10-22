using Acornima.Ast;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly AppRuleManager _appRuleManager;
        private readonly INavigatorService _navigatorService;
        private readonly IApplicationStateService _applicationStateService;

        public UserManager(
            IUserService userService,
            AppRuleManager appRuleManager,
            INavigatorService navigatorService,
            IApplicationStateService applicationStateService)
        {
            _userService = userService;
            _appRuleManager = appRuleManager;
            _navigatorService = navigatorService;
            _applicationStateService = applicationStateService;
        }

        public async Task<bool> Login(string pinCode)
        {
            var user = await _userService.GetUserAsync(x => x.PinCode == pinCode);
            if (user != null)
            {
                _applicationStateService.CurrentLoggedInUser =user;
                _applicationStateService.SetTextBlockUsername();

                _applicationStateService.SetGridBottomBarVisibility(VisibilityState.Visible);
                _navigatorService.Navigate("Navigation");

                // Kullanıcı giriş yaptı, kuralları tetikleyelim
                await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserLoggedIn, user);

                return true;
            }

            // Giriş başarısız, başarısız giriş kuralları tetiklenebilir
            await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserFailedToLogin);
            return false;
        }

        public async Task<bool> Logout()
        {
            _applicationStateService.SetTextBlockUsername();
            _applicationStateService.SetGridBottomBarVisibility(VisibilityState.Collapsed);
            _navigatorService.Navigate("Login");

            await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserLoggedOut, _applicationStateService.CurrentLoggedInUser);
            _applicationStateService.CurrentLoggedInUser = null;
            return true;
        }

        public async Task<string> GetUserNameByPinCode(string pinCode)
        {
            var user = await _userService.GetUserAsync(x => x.PinCode == pinCode);
            if (user != null)
                return user.Name ?? "-";
            else
                return "-";
        }
        public async Task<UserDTO> GetUserByPinCode(string pinCode)
        {
            var user = await _userService.GetUserAsync(x => x.PinCode == pinCode);
            if (user != null)
                return user;
            else
                return null;
        }
    }
}
