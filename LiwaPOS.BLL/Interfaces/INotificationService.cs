namespace LiwaPOS.BLL.Interfaces
{
    public interface INotificationService
    {
        void ShowMessage(string message, string? title = null, string? icon = null, string? button = null);
    }
}
