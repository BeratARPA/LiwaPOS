using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using LiwaPOS.WpfAppUI.Extensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Dinamik parametreleri ayarlamak için kullanılacak metod
        public virtual void SetParameter(dynamic parameter)
        {
            // Her ViewModel kendi özel ihtiyaçlarına göre bu metodu geçersiz kılabilir
        }

        protected async Task<bool> ValidateFieldsAsync(List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)> validations, ICustomNotificationService customNotificationService)
        {
            var errorMessages = new List<string>();

            foreach (var validation in validations)
            {
                if (!await validation.ValidationFunction.Invoke())
                {
                    errorMessages.Add(validation.Message);
                }
            }

            if (errorMessages.Any())
            {
                customNotificationService.ShowNotification(new NotificationDTO
                {
                    Name = "Validation Notification",
                    Title = await TranslatorExtension.TranslateUI("Warning"),
                    Message = string.Join("\n", errorMessages),
                    ButtonType = NotificationButtonType.None,
                    Position = NotificationPosition.TopRight,
                    Icon = NotificationIcon.Warning
                });

                return false;
            }

            return true;
        }
    }
}
