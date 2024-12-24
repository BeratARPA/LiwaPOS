using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class AutomationCommandManagementViewModel : ViewModelBase
    {
        private readonly IAutomationCommandService _automationCommandService;
        private readonly IAutomationCommandMapService _automationCommandMapService;
        private readonly ITerminalService _terminalService;
        private readonly IDepartmentService _departmentService;
        private readonly ICustomNotificationService _customNotificationService;

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

        private string _color;

        public string Color
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


        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public AutomationCommandManagementViewModel(IAutomationCommandService automationCommandService, IAutomationCommandMapService automationCommandMapService, ITerminalService terminalService, IDepartmentService departmentService, ICustomNotificationService customNotificationService)
        {
            _automationCommandService = automationCommandService;
            _automationCommandMapService = automationCommandMapService;
            _terminalService = terminalService;
            _departmentService = departmentService;
            _customNotificationService = customNotificationService;

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is AutomationCommandDTO automationCommand)
            {
                AutomationCommandId = automationCommand.Id;
                Category = automationCommand.Category;
                ButtonHeader = automationCommand.ButtonHeader;
                Color = automationCommand.Color;
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
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
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
                existingAutomationCommand.Color = Color;
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
            }
            else
            {
                var automationCommand = new AutomationCommandDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Category = Category,
                    ButtonHeader = ButtonHeader,
                    Color = Color,
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
            }

            GlobalVariables.Navigator.Navigate("AutomationCommands");
        }
    }
}
