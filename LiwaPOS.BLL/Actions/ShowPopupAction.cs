using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class ShowPopupAction : IAction
    {
        private readonly ICustomNotificationService _customNotificationService;

        public ShowPopupAction(ICustomNotificationService customNotificationService)
        {
            _customNotificationService = customNotificationService;
        }

        public async Task<object> Execute(string properties)
        {
            var popupProperties = JsonHelper.Deserialize<NotificationDTO>(properties);
            if (popupProperties == null)
                return false;

            var result = _customNotificationService.ShowNotification(popupProperties);
            return result;
        }
    }
}
