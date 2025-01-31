using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class AutomationCommandManagementViewModel : ViewModelBase
    {

        private readonly IAutomationCommandService _automationCommandService;
        private readonly IAutomationCommandMapService _automationCommandMapService;
        private readonly ITerminalService _terminalService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserRoleService _userRoleService;
        //private readonly ITicketTypeService _ticketTypeService;
        private readonly ICustomNotificationService _customNotificationService;

        #region Properties      
        private ObservableCollection<AutomationCommandMapDTO> _maps;
        public ObservableCollection<AutomationCommandMapDTO> Maps
        {
            get => _maps;
            set
            {
                _maps = value;
                OnPropertyChanged(nameof(Maps));
            }
        }

        private ObservableCollection<TerminalDTO> _terminals;
        public ObservableCollection<TerminalDTO> Terminals
        {
            get => _terminals;
            set
            {
                _terminals = value;
                OnPropertyChanged(nameof(Terminals));
            }
        }

        private ObservableCollection<DepartmentDTO> _departments;
        public ObservableCollection<DepartmentDTO> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
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

        private ObservableCollection<TicketTypeDTO> _ticketTypes;
        public ObservableCollection<TicketTypeDTO> TicketTypes
        {
            get => _ticketTypes;
            set
            {
                _ticketTypes = value;
                OnPropertyChanged(nameof(TicketTypes));
            }
        }

        private ObservableCollection<string> _screenVisibilityTypes;
        public ObservableCollection<string> ScreenVisibilityTypes
        {
            get => _screenVisibilityTypes;
            set
            {
                _screenVisibilityTypes = value;
                OnPropertyChanged(nameof(ScreenVisibilityTypes));
            }
        }

        private AutomationCommandMapDTO _selectedMap;
        public AutomationCommandMapDTO SelectedMap
        {
            get => _selectedMap;
            set
            {
                _selectedMap = value;
                OnPropertyChanged(nameof(SelectedMap));
            }
        }

        private int _automationCommandId;
        public int AutomationCommandId
        {
            get { return _automationCommandId; }
            set { _automationCommandId = value; OnPropertyChanged(); }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }

        private string _buttonHeader;
        public string ButtonHeader
        {
            get { return _buttonHeader; }
            set { _buttonHeader = value; OnPropertyChanged(); }
        }

        private System.Windows.Media.Color _color;
        public System.Windows.Media.Color Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(); }
        }

        private int _fontSize;
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged(); }
        }

        private string _values;
        public string Values
        {
            get { return _values; }
            set { _values = value; OnPropertyChanged(); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        private bool _toggleValues;
        public bool ToggleValues
        {
            get { return _toggleValues; }
            set { _toggleValues = value; OnPropertyChanged(); }
        }

        private bool _executeOnce;
        public bool ExecuteOnce
        {
            get { return _executeOnce; }
            set { _executeOnce = value; OnPropertyChanged(); }
        }

        private bool _clearSelection;
        public bool ClearSelection
        {
            get { return _clearSelection; }
            set { _clearSelection = value; OnPropertyChanged(); }
        }

        private int _confirmationType;
        public int ConfirmationType
        {
            get { return _confirmationType; }
            set { _confirmationType = value; OnPropertyChanged(); }
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; OnPropertyChanged(); }
        }

        private string _contentTemplate;
        public string ContentTemplate
        {
            get { return _contentTemplate; }
            set { _contentTemplate = value; OnPropertyChanged(); }
        }

        private int _autoRefresh;
        public int AutoRefresh
        {
            get { return _autoRefresh; }
            set { _autoRefresh = value; OnPropertyChanged(); }
        }

        private int _tileCacheLifetime;
        public int TileCacheLifetime
        {
            get { return _tileCacheLifetime; }
            set { _tileCacheLifetime = value; OnPropertyChanged(); }
        }

        private string _navigationModule;
        public string NavigationModule
        {
            get { return _navigationModule; }
            set { _navigationModule = value; OnPropertyChanged(); }
        }

        private bool _askTextInput;
        public bool AskTextInput
        {
            get { return _askTextInput; }
            set { _askTextInput = value; OnPropertyChanged(); }
        }

        private bool _askNumericInput;
        public bool AskNumericInput
        {
            get { return _askNumericInput; }
            set { _askNumericInput = value; OnPropertyChanged(); }
        }

        private string _automationCommandName;
        public string AutomationCommandName
        {
            get { return _automationCommandName; }
            set { _automationCommandName = value; OnPropertyChanged(); }
        }
        #endregion

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand AddMapCommand { get; }
        public ICommand DeleteMapCommand { get; }

        public AutomationCommandManagementViewModel(IAutomationCommandService automationCommandService, IAutomationCommandMapService automationCommandMapService, ITerminalService terminalService, IDepartmentService departmentService, IUserRoleService userRoleService, ICustomNotificationService customNotificationService)
        {
            _automationCommandService = automationCommandService;
            _automationCommandMapService = automationCommandMapService;
            _terminalService = terminalService;
            _departmentService = departmentService;
            _userRoleService = userRoleService;
            _customNotificationService = customNotificationService;

           _= LoadInitialDataAsync();

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
            AddMapCommand = new AsyncRelayCommand(AddMap);
            DeleteMapCommand = new AsyncRelayCommand(DeleteMap, CanDelete);   
        }

        public async void SetParameter(dynamic parameter)
        {
            if (parameter is AutomationCommandDTO automationCommand)
            {
                AutomationCommandId = automationCommand.Id;
                Category = automationCommand.Category;
                ButtonHeader = automationCommand.ButtonHeader;
                Color = ColorConverterHelper.HexToColor(automationCommand.Color);
                FontSize = automationCommand.FontSize;
                Values = automationCommand.Values;
                Image = automationCommand.Image;
                ToggleValues = automationCommand.ToggleValues;
                ExecuteOnce = automationCommand.ExecuteOnce;
                ClearSelection = automationCommand.ClearSelection;
                ConfirmationType = automationCommand.ConfirmationType;
                Symbol = automationCommand.Symbol;
                ContentTemplate = automationCommand.ContentTemplate;
                AutoRefresh = automationCommand.AutoRefresh;
                TileCacheLifetime = automationCommand.TileCacheLifetime;
                NavigationModule = automationCommand.NavigationModule;
                AskTextInput = automationCommand.AskTextInput;
                AskNumericInput = automationCommand.AskNumericInput;
                AutomationCommandName = automationCommand.Name;

                var existingMaps = await _automationCommandMapService.GetAllAutomationCommandMapsAsync(x => x.AutomationCommandId == AutomationCommandId);
                Maps = new ObservableCollection<AutomationCommandMapDTO>(existingMaps);
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }
      
        private async Task LoadInitialDataAsync()
        {
            Maps = new ObservableCollection<AutomationCommandMapDTO>();
            Terminals = new ObservableCollection<TerminalDTO>(await _terminalService.GetAllTerminalsAsync());
            Departments = new ObservableCollection<DepartmentDTO>(await _departmentService.GetAllDepartmentsAsync());
            UserRoles = new ObservableCollection<UserRoleDTO>(await _userRoleService.GetAllUserRolesAsync());
            TicketTypes = new ObservableCollection<TicketTypeDTO>();
            ScreenVisibilityTypes = new ObservableCollection<string>(Enum.GetNames(typeof(ScreenVisibilityType)));
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("AutomationCommands");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(AutomationCommandName),
            async () => !string.IsNullOrEmpty(AutomationCommandName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(AutomationCommandName))
        ),
        (
            nameof(AutomationCommandName),
            async () => !(await _automationCommandService.GetAllAutomationCommandsAsNoTrackingAsync(x => x.Name == AutomationCommandName&& x.Id != AutomationCommandId)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(AutomationCommandName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var existingAutomationCommand = await _automationCommandService.GetAutomationCommandByIdAsNoTrackingAsync(AutomationCommandId);
            if (existingAutomationCommand != null)
            {
                existingAutomationCommand.Category = Category;
                existingAutomationCommand.ButtonHeader = ButtonHeader;
                existingAutomationCommand.Color = Color.ToString();
                existingAutomationCommand.FontSize = FontSize;
                existingAutomationCommand.Values = Values;
                existingAutomationCommand.Image = Image;
                existingAutomationCommand.ToggleValues = ToggleValues;
                existingAutomationCommand.ExecuteOnce = ExecuteOnce;
                existingAutomationCommand.ClearSelection = ClearSelection;
                existingAutomationCommand.ConfirmationType = ConfirmationType;
                existingAutomationCommand.Symbol = Symbol;
                existingAutomationCommand.ContentTemplate = ContentTemplate;
                existingAutomationCommand.AutoRefresh = AutoRefresh;
                existingAutomationCommand.TileCacheLifetime = TileCacheLifetime;
                existingAutomationCommand.NavigationModule = NavigationModule;
                existingAutomationCommand.AskTextInput = AskTextInput;
                existingAutomationCommand.AskNumericInput = AskNumericInput;
                existingAutomationCommand.Name = AutomationCommandName;

                await _automationCommandService.UpdateAutomationCommandAsync(existingAutomationCommand);

                var existingAutomationCommandMaps = await _automationCommandMapService.GetAllAutomationCommandMapsAsNoTrackingAsync(x => x.AutomationCommandId == existingAutomationCommand.Id);
                foreach (var existingAutomationCommandMap in existingAutomationCommandMaps)
                {
                    await _automationCommandMapService.DeleteAutomationCommandMapAsync(existingAutomationCommandMap.Id);
                }

                foreach (var map in Maps)
                {
                    var automationCommandMap = new AutomationCommandMapDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        TerminalId = map.TerminalId ?? 0,
                        DepartmentId = map.DepartmentId ?? 0,
                        UserRoleId = map.UserRoleId ?? 0,
                        TicketTypeId = map.TicketTypeId ?? 0,
                        AutomationCommandId = existingAutomationCommand.Id,
                        DisplayOn = map.DisplayOn ?? ScreenVisibilityType.Ticket.ToString(),
                        EnabledStates = map.EnabledStates ?? "*",
                        VisibleStates = map.VisibleStates ?? "*"
                    };

                    await _automationCommandMapService.AddAutomationCommandMapAsync(automationCommandMap);
                }
            }
            else
            {
                var automationCommand = new AutomationCommandDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Category = Category,
                    ButtonHeader = ButtonHeader,
                    Color = Color.ToString(),
                    FontSize = FontSize,
                    Values = Values,
                    Image = Image,
                    ToggleValues = ToggleValues,
                    ExecuteOnce = ExecuteOnce,
                    ClearSelection = ClearSelection,
                    ConfirmationType = ConfirmationType,
                    Symbol = Symbol,
                    ContentTemplate = ContentTemplate,
                    AutoRefresh = AutoRefresh,
                    TileCacheLifetime = TileCacheLifetime,
                    NavigationModule = NavigationModule,
                    AskTextInput = AskTextInput,
                    AskNumericInput = AskNumericInput,
                    Name = AutomationCommandName
                };

                await _automationCommandService.AddAutomationCommandAsync(automationCommand);

                var newAutomationCommand = await _automationCommandService.GetAutomationCommandAsNoTrackingAsync(x => x.EntityGuid == automationCommand.EntityGuid);

                foreach (var map in Maps)
                {
                    var automationCommandMap = new AutomationCommandMapDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        TerminalId = map.TerminalId ?? 0,
                        DepartmentId = map.DepartmentId ?? 0,
                        UserRoleId = map.UserRoleId ?? 0,
                        TicketTypeId = map.TicketTypeId ?? 0,
                        AutomationCommandId = newAutomationCommand.Id,
                        DisplayOn = map.DisplayOn ?? ScreenVisibilityType.Ticket.ToString(),
                        EnabledStates = map.EnabledStates ?? "*",
                        VisibleStates = map.VisibleStates ?? "*"
                    };

                    await _automationCommandMapService.AddAutomationCommandMapAsync(automationCommandMap);
                }
            }

            GlobalVariables.Navigator.Navigate("AutomationCommands");
        }

        #region Map
        private async Task AddMap(object obj)
        {
            var automationCommandMap = new AutomationCommandMapDTO
            {
                AutomationCommandId = null,
                TerminalId = null,
                DepartmentId = null,
                UserRoleId = null,
                TicketTypeId = null,
                DisplayOn = ScreenVisibilityType.Ticket.ToString(),
                EnabledStates = null,
                VisibleStates = null
            };

            Maps.Add(automationCommandMap);
            SelectedMap = automationCommandMap;
        }

        private async Task DeleteMap(object obj)
        {
            if (SelectedMap != null)
            {
                Maps.Remove(SelectedMap);
                SelectedMap = null;
            }
        }

        private bool CanDelete(object obj)
        {
            return SelectedMap != null;
        }
        #endregion
    }
}
