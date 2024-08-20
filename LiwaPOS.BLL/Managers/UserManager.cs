using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Enums;

namespace LiwaPOS.BLL.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly AppRuleManager _appRuleManager;
        private readonly IAppRuleService _appRuleService;

        public UserManager(
            IUserService userService,
            AppRuleManager appRuleManager,
            IAppRuleService appRuleService)
        {
            _userService = userService;
            _appRuleManager = appRuleManager;
            _appRuleService = appRuleService;
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
