using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;

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
                await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserLoggedIn);
                return true;
            }

            // Giriş başarısız, başarısız giriş kuralları tetiklenebilir
            await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserFailedToLogin);
            return false;
        }
    }
}
