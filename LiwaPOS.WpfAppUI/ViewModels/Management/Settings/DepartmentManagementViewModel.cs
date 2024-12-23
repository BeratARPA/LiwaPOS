using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Settings
{
    public class DepartmentManagementViewModel : ViewModelBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ICustomNotificationService _customNotificationService;
        private string _departmentName;
        private int _warehouseId;
        private int _screenMenuId;
        private int _departmentId;

        public int DepartmentId
        {
            get => _departmentId;
            set
            {
                _departmentId = value;
                OnPropertyChanged();
            }
        }

        public string DepartmentName
        {
            get => _departmentName;
            set
            {
                _departmentName = value;
                OnPropertyChanged();
            }
        }

        public int WarehouseId
        {
            get => _warehouseId;
            set
            {
                _warehouseId = value;
                OnPropertyChanged();
            }
        }

        public int ScreenMenuId
        {
            get => _screenMenuId;
            set
            {
                _screenMenuId = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public DepartmentManagementViewModel(IDepartmentService departmentService, ICustomNotificationService customNotificationService)
        {
            _departmentService = departmentService;
            _customNotificationService = customNotificationService;

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is DepartmentDTO department)
            {
                DepartmentId = department.Id;
                DepartmentName = department.Name;
                WarehouseId = department.WarehouseId;
                ScreenMenuId = department.ScreenMenuId;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("Departments");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(DepartmentName),
            async () => !string.IsNullOrEmpty(DepartmentName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(DepartmentName))
        ),
        (
            nameof(DepartmentName),
            async () => !(await _departmentService.GetAllDepartmentsAsNoTrackingAsync(x => x.Name == DepartmentName)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(DepartmentName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var existingDepartment = await _departmentService.GetDepartmentByIdAsNoTrackingAsync(DepartmentId);
            if (existingDepartment != null)
            {
                existingDepartment.Name = DepartmentName;
                existingDepartment.WarehouseId = WarehouseId;
                existingDepartment.ScreenMenuId = ScreenMenuId;

                await _departmentService.UpdateDepartmentAsync(existingDepartment);
            }
            else
            {
                var department = new DepartmentDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = DepartmentName,
                    WarehouseId = WarehouseId,
                    ScreenMenuId = ScreenMenuId
                };

                await _departmentService.AddDepartmentAsync(department);
            }

            GlobalVariables.Navigator.Navigate("Departments");
        }
    }
}
