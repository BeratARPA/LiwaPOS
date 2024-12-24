using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;

namespace LiwaPOS.BLL.Actions
{
    public class LogoutUserAction : IAction
    {
        private readonly UserManager _userManager;

        public LogoutUserAction(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<object> Execute(string properties)
        {
            bool isSuccessful = await _userManager.Logout();
            return isSuccessful;
        }
    }
}
