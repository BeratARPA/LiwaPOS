using LiwaPOS.BLL.Interfaces;
using System.Text.Json;

namespace LiwaPOS.BLL.Actions
{
    public class ShowPopupAction : IAction
    {
        private readonly INotificationService _notificationService;

        public ShowPopupAction(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Execute(string properties)
        {
            // JSON verisini ayrıştır
            var popupProperties = JsonSerializer.Deserialize<PopupProperties>(properties);
            if (popupProperties != null)
                _notificationService.ShowMessage(popupProperties.Message, popupProperties.Title, popupProperties.Icon, popupProperties.Button);
        }
    }

    public class PopupProperties
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Icon { get; set; }
        public string? Button { get; set; }
    }
}
