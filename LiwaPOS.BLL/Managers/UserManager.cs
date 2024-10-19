using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly AppRuleManager _appRuleManager;

        public UserManager(
            IUserService userService,
            AppRuleManager appRuleManager)
        {
            _userService = userService;
            _appRuleManager = appRuleManager;
        }

        public async Task<bool> Login(string pinCode)
        {
            var user = await _userService.GetUserAsync(x => x.PinCode == pinCode);
            if (user != null)
            {
                // Kullanıcı giriş yaptı, kuralları tetikleyelim
                await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserLoggedIn, user);
                return true;
            }

            // Giriş başarısız, başarısız giriş kuralları tetiklenebilir
            await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserFailedToLogin);
            return false;
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
