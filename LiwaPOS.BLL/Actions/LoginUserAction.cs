using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class LoginUserAction : IAction
    {
        private readonly UserManager _userManager;

        public LoginUserAction(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<object> Execute(string properties)
        {
            var loginUserProperties = JsonHelper.Deserialize<LoginUserDTO>(properties);
            if (loginUserProperties == null)
                return false;

            bool isSuccessful = await _userManager.Login(loginUserProperties.PinCode);
            return isSuccessful;
        }
    }
}
