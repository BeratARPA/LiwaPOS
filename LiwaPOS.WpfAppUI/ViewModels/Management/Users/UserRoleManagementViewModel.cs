using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Users
{
    public class UserRoleManagementViewModel : ViewModelBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomNotificationService _customNotificationService;

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

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        #region Permissions
        private bool _exitFullscreen;
        public bool ExitFullscreen
        {
            get { return _exitFullscreen; }
            set { _exitFullscreen = value; OnPropertyChanged(); }
        }

        private bool _viewAccountScreen;
        public bool ViewAccountScreen
        {
            get { return _viewAccountScreen; }
            set { _viewAccountScreen = value; OnPropertyChanged(); }
        }

        private bool _viewStockNavigation;
        public bool ViewStockNavigation
        {
            get { return _viewStockNavigation; }
            set { _viewStockNavigation = value; OnPropertyChanged(); }
        }

        private bool _accessManagement;
        public bool AccessManagement
        {
            get { return _accessManagement; }
            set { _accessManagement = value; OnPropertyChanged(); }
        }

        private bool _viewMarketNavigation;
        public bool ViewMarketNavigation
        {
            get { return _viewMarketNavigation; }
            set { _viewMarketNavigation = value; OnPropertyChanged(); }
        }

        private bool _accessNavigation;
        public bool AccessNavigation
        {
            get { return _accessNavigation; }
            set { _accessNavigation = value; OnPropertyChanged(); }
        }

        private bool _performEndOfDay;
        public bool PerformEndOfDay
        {
            get { return _performEndOfDay; }
            set { _performEndOfDay = value; OnPropertyChanged(); }
        }

        private bool _adminPinApproval;
        public bool AdminPinApproval
        {
            get { return _adminPinApproval; }
            set { _adminPinApproval = value; OnPropertyChanged(); }
        }

        private bool _openReports;
        public bool OpenReports
        {
            get { return _openReports; }
            set { _openReports = value; OnPropertyChanged(); }
        }

        // Hesap İzinleri
        private bool _createAccount;
        public bool CreateAccount
        {
            get { return _createAccount; }
            set { _createAccount = value; OnPropertyChanged(); }
        }

        // Adisyon İzinleri
        private bool _unlockBill;
        public bool UnlockBill
        {
            get { return _unlockBill; }
            set { _unlockBill = value; OnPropertyChanged(); }
        }

        private bool _removeBillTag;
        public bool RemoveBillTag
        {
            get { return _removeBillTag; }
            set { _removeBillTag = value; OnPropertyChanged(); }
        }

        private bool _mergeBills;
        public bool MergeBills
        {
            get { return _mergeBills; }
            set { _mergeBills = value; OnPropertyChanged(); }
        }

        private bool _viewOldBills;
        public bool ViewOldBills
        {
            get { return _viewOldBills; }
            set { _viewOldBills = value; OnPropertyChanged(); }
        }

        private bool _addExtraFeature;
        public bool AddExtraFeature
        {
            get { return _addExtraFeature; }
            set { _addExtraFeature = value; OnPropertyChanged(); }
        }

        private bool _viewOthersDocuments;
        public bool ViewOthersDocuments
        {
            get { return _viewOthersDocuments; }
            set { _viewOthersDocuments = value; OnPropertyChanged(); }
        }

        private bool _modifyBillExistence;
        public bool ModifyBillExistence
        {
            get { return _modifyBillExistence; }
            set { _modifyBillExistence = value; OnPropertyChanged(); }
        }

        private bool _changeOrderQuantity;
        public bool ChangeOrderQuantity
        {
            get { return _changeOrderQuantity; }
            set { _changeOrderQuantity = value; OnPropertyChanged(); }
        }

        private bool _changeSelectedOrderQuantity;
        public bool ChangeSelectedOrderQuantity
        {
            get { return _changeSelectedOrderQuantity; }
            set { _changeSelectedOrderQuantity = value; OnPropertyChanged(); }
        }

        private bool _allowRefund;
        public bool AllowRefund
        {
            get { return _allowRefund; }
            set { _allowRefund = value; OnPropertyChanged(); }
        }

        private bool _viewOpenBills;
        public bool ViewOpenBills
        {
            get { return _viewOpenBills; }
            set { _viewOpenBills = value; OnPropertyChanged(); }
        }

        private bool _viewBillsDuringPayment;
        public bool ViewBillsDuringPayment
        {
            get { return _viewBillsDuringPayment; }
            set { _viewBillsDuringPayment = value; OnPropertyChanged(); }
        }

        private bool _closeTips;
        public bool CloseTips
        {
            get { return _closeTips; }
            set { _closeTips = value; OnPropertyChanged(); }
        }

        private bool _canPerformEndOfDay;
        public bool CanPerformEndOfDay
        {
            get { return _canPerformEndOfDay; }
            set { _canPerformEndOfDay = value; OnPropertyChanged(); }
        }

        // Rapor İzinleri
        private bool _changeReportDate;
        public bool ChangeReportDate
        {
            get { return _changeReportDate; }
            set { _changeReportDate = value; OnPropertyChanged(); }
        }

        // Departman İzinleri
        private bool _changeDepartment;
        public bool ChangeDepartment
        {
            get { return _changeDepartment; }
            set { _changeDepartment = value; OnPropertyChanged(); }
        }

        private async Task LoadPermissionsAsync()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync(x => x.UserRoleId == UserRoleId);

            // Reflection kullanarak özellikleri dinamik olarak dolduruyoruz.
            foreach (var permission in permissions)
            {
                var property = GetType().GetProperty(permission.Name);

                if (property != null && property.PropertyType == typeof(bool))
                {
                    property.SetValue(this, permission.Value);
                }
            }
        }

        private Dictionary<string, bool> _permissionValues = new();
        #endregion

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public UserRoleManagementViewModel(IPermissionService permissionService, IUserRoleService userRoleService, ICustomNotificationService customNotificationService)
        {
            _permissionService = permissionService;
            _userRoleService = userRoleService;
            _customNotificationService = customNotificationService;

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is UserRoleDTO userRole)
            {
                UserRoleId = userRole.Id;
                Name = userRole.Name;
                IsAdmin = userRole.IsAdmin;

                _ = LoadPermissionsAsync();
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("UserRoles");
        }

        private void CollectPermissions()
        {
            _permissionValues.Clear();

            // Reflection kullanarak tüm boolean özellikleri dinamik olarak topluyoruz.
            var properties = GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(bool));

            foreach (var property in properties)
            {
                var value = (bool)property.GetValue(this);
                _permissionValues[property.Name] = value;
            }
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
            nameof(Name),
            async () => !(await _userRoleService.GetAllUserRolesAsNoTrackingAsync(x => x.Name == Name && x.Id != UserRoleId)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(Name))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            CollectPermissions();

            var existingUserRole = await _userRoleService.GetUserRoleByIdAsNoTrackingAsync(UserRoleId);
            if (existingUserRole != null)
            {
                existingUserRole.Name = Name;
                existingUserRole.IsAdmin = IsAdmin;

                await _userRoleService.UpdateUserRoleAsync(existingUserRole);

                var existingPermissions = await _permissionService.GetAllPermissionsAsNoTrackingAsync(x => x.UserRoleId == UserRoleId);
                foreach (var existingPermission in existingPermissions)
                {
                    await _permissionService.DeletePermissionAsync(existingPermission.Id);
                }

                foreach (var permissionValue in _permissionValues)
                {
                    var permission = new PermissionDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        UserRoleId = UserRoleId,
                        Name = permissionValue.Key,
                        Value = permissionValue.Value
                    };

                    await _permissionService.AddPermissionAsync(permission);
                }
            }
            else
            {
                var userRole = new UserRoleDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = Name,
                    IsAdmin = IsAdmin
                };

                await _userRoleService.AddUserRoleAsync(userRole);

                var newUserRole = await _userRoleService.GetUserRoleAsNoTrackingAsync(x => x.EntityGuid == userRole.EntityGuid);

                foreach (var permissionValue in _permissionValues)
                {
                    var permission = new PermissionDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        UserRoleId = newUserRole.Id,
                        Name = permissionValue.Key,
                        Value = permissionValue.Value
                    };

                    await _permissionService.AddPermissionAsync(permission);
                }
            }

            GlobalVariables.Navigator.Navigate("UserRoles");
        }
    }
}