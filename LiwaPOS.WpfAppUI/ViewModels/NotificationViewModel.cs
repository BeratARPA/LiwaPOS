using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using LiwaPOS.WpfAppUI.Commands;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private NotificationDTO _notification;
        public Action<bool> CloseAction { get; set; }

        public NotificationViewModel(NotificationDTO notification = null)
        {
            _notification = notification;
            if (notification == null)
                _notification = new NotificationDTO();

            YesCommand = new AsyncRelayCommand(ExecuteYes);
            NoCommand = new AsyncRelayCommand(ExecuteNo);
            OkCommand = new AsyncRelayCommand(ExecuteOk);
            CancelCommand = new AsyncRelayCommand(ExecuteCancel);

            SetButtonVisibility(_notification.ButtonType);
        }

        public string Title => _notification.Title;
        public string Message => _notification.Message;
        public NotificationIcon Icon => _notification.Icon;
        public NotificationPosition Position => _notification.Position;
        public NotificationButtonType Button => _notification.ButtonType;
        public int DisplayDurationInSecond => _notification.DisplayDurationInSecond;
        public bool IsDialog => _notification.IsDialog;

        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        private bool _isYesVisible;
        private bool _isNoVisible;
        private bool _isOkVisible;
        private bool _isCancelVisible;

        public bool IsYesVisible
        {
            get => _isYesVisible;
            set
            {
                if (_isYesVisible != value)
                {
                    _isYesVisible = value;
                    OnPropertyChanged(nameof(IsYesVisible));
                }
            }
        }

        public bool IsNoVisible
        {
            get => _isNoVisible;
            set
            {
                if (_isNoVisible != value)
                {
                    _isNoVisible = value;
                    OnPropertyChanged(nameof(IsNoVisible));
                }
            }
        }

        public bool IsOkVisible
        {
            get => _isOkVisible;
            set
            {
                if (_isOkVisible != value)
                {
                    _isOkVisible = value;
                    OnPropertyChanged(nameof(IsOkVisible));
                }
            }
        }

        public bool IsCancelVisible
        {
            get => _isCancelVisible;
            set
            {
                if (_isCancelVisible != value)
                {
                    _isCancelVisible = value;
                    OnPropertyChanged(nameof(IsCancelVisible));
                }
            }
        }

        private void SetButtonVisibility(NotificationButtonType buttonType)
        {
            if (buttonType == NotificationButtonType.None)
                return;

            IsYesVisible = buttonType == NotificationButtonType.YesNo || buttonType == NotificationButtonType.YesNoCancel;
            IsNoVisible = buttonType == NotificationButtonType.YesNo || buttonType == NotificationButtonType.YesNoCancel;
            IsOkVisible = buttonType == NotificationButtonType.OK || buttonType == NotificationButtonType.OkCancel;
            IsCancelVisible = buttonType == NotificationButtonType.OkCancel || buttonType == NotificationButtonType.YesNoCancel;
        }

        private async Task ExecuteYes(object parameter)
        {
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CloseAction?.Invoke(true);
            });
        }

        private async Task ExecuteNo(object parameter)
        {
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CloseAction?.Invoke(false);
            });
        }

        private async Task ExecuteOk(object parameter)
        {
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CloseAction?.Invoke(true);
            });
        }

        private async Task ExecuteCancel(object parameter)
        {
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CloseAction?.Invoke(false);
            });
        }        
    }
}
