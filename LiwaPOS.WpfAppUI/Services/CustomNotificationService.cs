using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using LiwaPOS.WpfAppUI.ViewModels;
using LiwaPOS.WpfAppUI.Views;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Services
{
    public class CustomNotificationService : ICustomNotificationService
    {
        // Her pozisyon için ayrı bir aktif bildirim listesi
        private static readonly Dictionary<NotificationPosition, List<NotificationWindow>> _activeNotificationsByPosition = new()
    {
        { NotificationPosition.BottomRight, new List<NotificationWindow>() },
        { NotificationPosition.BottomLeft, new List<NotificationWindow>() },
        { NotificationPosition.TopRight, new List<NotificationWindow>() },
        { NotificationPosition.TopLeft, new List<NotificationWindow>() },
        { NotificationPosition.Center, new List<NotificationWindow>() }
    };

        public void ShowNotification(NotificationDTO notification)
        {
            var notificationWindow = new NotificationWindow(new NotificationViewModel(notification));

            notificationWindow.Loaded += (s, e) => ArrangeNotificationPosition(notificationWindow, notification.Position);
            notificationWindow.Closed += (s, e) =>
            {
                _activeNotificationsByPosition[notification.Position].Remove(notificationWindow);
                RearrangeNotifications(notification.Position);
            };

            _activeNotificationsByPosition[notification.Position].Add(notificationWindow);

            if (notification.IsDialog)
                notificationWindow.ShowDialog();
            else
                notificationWindow.Show();
        }

        private void ArrangeNotificationPosition(NotificationWindow notificationWindow, NotificationPosition position)
        {
            double offset = 10; // Ekran kenarından ve bildirimler arasındaki mesafe
            double windowHeight = notificationWindow.Height + offset;
            double windowWidth = notificationWindow.Width + offset;

            double top = 0;
            double left = 0;

            var screenHeight = SystemParameters.PrimaryScreenHeight;
            var screenWidth = SystemParameters.PrimaryScreenWidth;

            // İlgili pozisyondaki bildirimlerin listesi
            var notifications = _activeNotificationsByPosition[position];
            int notificationIndex = notifications.IndexOf(notificationWindow);

            switch (position)
            {
                case NotificationPosition.BottomRight:
                    left = screenWidth - windowWidth - offset; // Sağ kenardan offset kadar mesafe bırak
                    top = screenHeight - (windowHeight * (notificationIndex + 1)) - offset; // Alt kenardan offset kadar mesafe bırak
                    break;
                case NotificationPosition.BottomLeft:
                    left = offset; // Sol kenardan offset kadar mesafe bırak
                    top = screenHeight - (windowHeight * (notificationIndex + 1)) - offset; // Alt kenardan offset kadar mesafe bırak
                    break;
                case NotificationPosition.TopRight:
                    left = screenWidth - windowWidth - offset; // Sağ kenardan offset kadar mesafe bırak
                    top = (windowHeight * notificationIndex) + offset; // Üst kenardan offset kadar mesafe bırak
                    break;
                case NotificationPosition.TopLeft:
                    left = offset; // Sol kenardan offset kadar mesafe bırak
                    top = (windowHeight * notificationIndex) + offset; // Üst kenardan offset kadar mesafe bırak
                    break;
                case NotificationPosition.Center:
                    left = (screenWidth - notificationWindow.Width) / 2;
                    top = (screenHeight - notificationWindow.Height) / 2;
                    break;
            }

            notificationWindow.Left = left;
            notificationWindow.Top = top;
        }

        private void RearrangeNotifications(NotificationPosition position)
        {
            // Kapanan bildirimden sonra, ilgili pozisyondaki diğer bildirimlerin yerini güncelle
            var notifications = _activeNotificationsByPosition[position];
            for (int i = 0; i < notifications.Count; i++)
            {
                ArrangeNotificationPosition(notifications[i], position);
            }
        }
    }
}
