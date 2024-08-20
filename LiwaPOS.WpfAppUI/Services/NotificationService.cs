using LiwaPOS.BLL.Interfaces;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NotificationService : INotificationService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Bilgilendirme", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
