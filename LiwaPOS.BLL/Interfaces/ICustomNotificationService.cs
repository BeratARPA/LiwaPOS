using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Interfaces
{
    public interface ICustomNotificationService
    {
       bool ShowNotification(NotificationDTO notification);       
    }
}
