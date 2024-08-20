using LiwaPOS.BLL.Interfaces;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NotificationService : INotificationService
    {
        public void ShowMessage(string message, string? title = null, string? icon = null, string? button = null)
        {
            MessageBoxImage messageBoxIcon = MessageBoxImage.None;
            switch (icon)
            {
                case "Information":
                    messageBoxIcon = MessageBoxImage.Information;
                    break;
                case "Warning":
                    messageBoxIcon = MessageBoxImage.Warning;
                    break;
                case "Error":
                    messageBoxIcon = MessageBoxImage.Error;
                    break;
                case "Question":
                    messageBoxIcon = MessageBoxImage.Question;
                    break;
                case "Hand":
                    messageBoxIcon = MessageBoxImage.Hand;
                    break;
                case "Stop":
                    messageBoxIcon = MessageBoxImage.Stop;
                    break;
                case "Exclamation":
                    messageBoxIcon = MessageBoxImage.Exclamation;
                    break;
                case "Asterisk":
                    messageBoxIcon = MessageBoxImage.Asterisk;
                    break;
                case "None":
                    messageBoxIcon = MessageBoxImage.None;
                    break;
                default:
                    messageBoxIcon = MessageBoxImage.None;
                    break;
            }

            MessageBoxButton messageBoxButton = MessageBoxButton.OK;
            switch (button)
            {
                case "OK":
                    messageBoxButton = MessageBoxButton.OK;
                    break;
                case "OKCancel":
                    messageBoxButton = MessageBoxButton.OKCancel;
                    break;
                case "YesNo":
                    messageBoxButton = MessageBoxButton.YesNo;
                    break;
                case "YesNoCancel":
                    messageBoxButton = MessageBoxButton.YesNoCancel;
                    break;
                default:
                    messageBoxButton = MessageBoxButton.OK;
                    break;
            }
            MessageBox.Show(message, title, messageBoxButton, messageBoxIcon);
        }
    }
}
