using LiwaPOS.BLL.Managers;
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
            bool loginSuccessful = await _userManager.Login(PinCode);
            Message = loginSuccessful ? "Giriş başarılı!" : "Giriş başarısız. Pin kodunu kontrol edin.";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
