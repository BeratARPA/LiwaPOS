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
                await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.UserLoggedIn);
                return true;
            }

            return false;
        }
    }
}
