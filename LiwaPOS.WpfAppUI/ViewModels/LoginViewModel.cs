using LiwaPOS.BLL.Managers;
using LiwaPOS.Entities.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;
        private readonly AppRuleManager _appRuleManager;
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

        public LoginViewModel(UserManager userManager, AppRuleManager appRuleManager)
        {
            _userManager = userManager;
            _appRuleManager = appRuleManager;
        }

        public async Task LoginAsync()
        {
         await _userManager.Login(PinCode);           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
