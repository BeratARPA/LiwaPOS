using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using System.Text.Json;

namespace LiwaPOS.BLL.Actions
{
    public class ShowPopupAction : IAction
    {
        private readonly ICustomNotificationService _customNotificationService;

        public ShowPopupAction(ICustomNotificationService customNotificationService)
        {
            _customNotificationService = customNotificationService;
        }

        public void Execute(string properties)
        {
            // JSON verisini ayrıştır
            var popupProperties = JsonHelper.Deserialize<NotificationDTO>(properties);
            if (popupProperties != null)
                _customNotificationService.ShowNotification(popupProperties);
        }
    }
}
