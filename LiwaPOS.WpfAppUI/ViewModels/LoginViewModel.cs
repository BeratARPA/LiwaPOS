using LiwaPOS.BLL.Managers;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Interfaces;
using LiwaPOS.WpfAppUI.UserControls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IApplicationStateService _applicationStateService;
        private readonly UserManager _userManager;
        private string _pinCode;
        private string _message;

        public string PinCode
        {
            get => _pinCode;
            set
            {
                _pinCode = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(IApplicationStateService applicationStateService, UserManager userManager)
        {
            _applicationStateService = applicationStateService;
            _userManager = userManager;
        }

        public async Task LoginAsync()
        {
            bool isSuccessful = await _userManager.Login(PinCode);
            if (isSuccessful)
            {
                _applicationStateService.CurrentLoggedInUser = await _userManager.GetUserByPinCode(PinCode);
                GlobalVariables.Shell.TextBlockUsername.Text = _applicationStateService.CurrentLoggedInUser == null ? "-" : _applicationStateService.CurrentLoggedInUser.Name ?? "-";

                GlobalVariables.Shell.GridBottomBar.Visibility = Visibility.Visible;
                GlobalVariables.Navigator.Navigate(typeof(NavigationUserControl));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
