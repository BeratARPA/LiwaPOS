using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IApplicationStateService
    {
        UserDTO CurrentLoggedInUser { get;}
        bool IsLocked { get;}
        AppScreenType ActiveAppScreen { get;}
        TerminalDTO CurrentTerminal { get; }
      
        Task SetCurrentUser(UserDTO? user);
        void SetTextBlockUsername();
        void SetGridBottomBarVisibility(VisibilityState visibilityState);
        Task SetIsLocked(bool value);
        void SetActiveAppScreen(string viewName);
        Task SetCurrentTerminal(string terminalName);

        Task<UserDTO> GetCurrentUser();
        Task<TerminalDTO> GetCurrentTerminal();
    }
}
