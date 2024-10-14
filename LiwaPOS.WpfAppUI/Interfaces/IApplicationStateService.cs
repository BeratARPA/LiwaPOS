using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.WpfAppUI.Interfaces
{
    public interface IApplicationStateService
    {
        UserDTO CurrentLoggedInUser { get; set; }
        bool IsLocked { get; set; }
    }
}
