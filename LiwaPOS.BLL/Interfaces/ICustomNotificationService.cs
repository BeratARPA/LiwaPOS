using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Interfaces
{
    public interface ICustomNotificationService
    {
       void ShowNotification(NotificationDTO notification);       
    }
}
