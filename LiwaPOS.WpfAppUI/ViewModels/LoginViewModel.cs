using LiwaPOS.BLL.Managers;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
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

        public LoginViewModel(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task LoginAsync()
        {
           bool isSuccessful= await _userManager.Login(PinCode);
            if (isSuccessful)
                GlobalVariables.Navigator.Navigate(typeof(NavigationUserControl));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
