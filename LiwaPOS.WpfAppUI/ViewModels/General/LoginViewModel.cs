using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class LoginViewModel : ViewModelBase
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
        }
    }
}
