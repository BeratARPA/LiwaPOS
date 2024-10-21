using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IApplicationStateService
    {
        UserDTO CurrentLoggedInUser { get; set; }
        bool IsLocked { get; set; }
        void SetTextBlockUsername();
        void SetGridBottomBarVisibility(VisibilityState visibilityState);
    }
}
