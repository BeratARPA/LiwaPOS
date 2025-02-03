using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models
{
    public class NotificationDTO
    {
        public string? Name { get; set; } = "Notification";
        public string? Title { get; set; } = "";
        public string? Message { get; set; } = "";
        public NotificationButtonType ButtonType { get; set; } = 0;
        public NotificationPosition Position { get; set; } = 0;
        public NotificationIcon Icon { get; set; } = 0;
        public int DisplayDurationInSecond { get; set; } = 5;
        public bool IsDialog { get; set; } = false;
    }
}
