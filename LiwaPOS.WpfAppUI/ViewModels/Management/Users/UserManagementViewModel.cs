using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Users
{
    public class UserManagementViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomNotificationService _customNotificationService;

        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(); }
        }

        private int _userRoleId;

        public int UserRoleId
        {
            get { return _userRoleId; }
            set { _userRoleId = value; OnPropertyChanged(); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _pinCode;

        public string PinCode
        {
            get { return _pinCode; }
            set { _pinCode = value; OnPropertyChanged(); }
        }

        private UserRoleDTO _selectedUserRole;
        public UserRoleDTO SelectedUserRole
        {
            get => _selectedUserRole;
            set
            {
                _selectedUserRole = value;
                _userRoleId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<UserRoleDTO> _userRoles;
        public ObservableCollection<UserRoleDTO> UserRoles
        {
            get => _userRoles;
            set
            {
                _userRoles = value;
                OnPropertyChanged(nameof(UserRoles));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public UserManagementViewModel(IUserService userService, IUserRoleService userRoleService, ICustomNotificationService customNotificationService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _customNotificationService = customNotificationService;

            _ = LoadUserRolesAsync();

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        private async Task LoadUserRolesAsync()
        {
            var data = await GetUserRoles();
            UserRoles = new ObservableCollection<UserRoleDTO>(data);

            SelectedUserRole = UserRoles.FirstOrDefault(t => t.Id == UserRoleId);
        }

        private async Task<IEnumerable<UserRoleDTO>> GetUserRoles()
        {
            return await _userRoleService.GetAllUserRolesAsync();
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is UserDTO user)
            {
                UserId = user.Id;
                Name = user.Name;
                PinCode = user.PinCode;
                UserRoleId = user.UserRoleId;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("Users");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(Name),
            async () => !string.IsNullOrEmpty(Name),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(Name))
        ),
        (
            nameof(PinCode),
            async () => !string.IsNullOrEmpty(PinCode),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(PinCode))
        ),
        (
            nameof(UserRoleId),
            async () => UserRoleId > 0,
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(UserRoleId))
        ),
        (
            nameof(PinCode),
            async () => !(await _userService.GetAllUsersAsNoTrackingAsync(x => x.PinCode == PinCode && x.Id != UserId)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(PinCode))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var existingUser = await _userService.GetUserByIdAsNoTrackingAsync(UserId);
            if (existingUser != null)
            {
                existingUser.Name = Name;
                existingUser.PinCode = PinCode;
                existingUser.UserRoleId = UserRoleId;

                await _userService.UpdateUserAsync(existingUser);
            }
            else
            {
                var user = new UserDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = Name,
                    PinCode = PinCode,
                    UserRoleId = UserRoleId,
                };

                await _userService.AddUserAsync(user);
            }

            GlobalVariables.Navigator.Navigate("Users");
        }
    }
}
