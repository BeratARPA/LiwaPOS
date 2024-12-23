using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class AppActionManagementViewModel : ViewModelBase
    {
        private readonly IAppActionService _appActionService;
        private readonly ICustomNotificationService _customNotificationService;
        private int _appActionId;
        private string _appActionName;
        private ActionType _appActionType;
        private string _appActionProperties;
        public ObservableCollection<ActionType> ActionTypes { get; }
        private object _currentActionModel;
        public List<UIElement> DynamicInputs { get; private set; }

        public int AppActionId
        {
            get => _appActionId;
            set
            {
                _appActionId = value;
                OnPropertyChanged();
            }
        }

        public string AppActionName
        {
            get => _appActionName;
            set
            {
                _appActionName = value;
                OnPropertyChanged();
            }
        }

        public ActionType AppActionType
        {
            get => _appActionType;
            set
            {
                _appActionType = value;
                OnPropertyChanged();
                // Dinamik inputları eylem tipine göre güncellemek için metot çalıştırılabilir
                UpdateDynamicProperties();
            }
        }

        public string AppActionProperties
        {
            get => _appActionProperties;
            set
            {
                _appActionProperties = value;
                OnPropertyChanged();
                // Properties değiştiğinde yeni değerleri inputlara aktar
                ApplyPropertiesToModel();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public AppActionManagementViewModel(IAppActionService appActionService, ICustomNotificationService customNotificationService)
        {
            _appActionService = appActionService;
            _customNotificationService = customNotificationService;

            ActionTypes = new ObservableCollection<ActionType>(Enum.GetValues(typeof(ActionType)).Cast<ActionType>());
            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);

            UpdateDynamicProperties();
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("AppActions");
        }

        private void UpdateDynamicProperties()
        {
            // ActionType'a göre model oluştur
            switch (AppActionType)
            {
                case ActionType.LoginUser:
                    _currentActionModel = new LoginUserDTO();
                    break;
                case ActionType.ShowPopup:
                    _currentActionModel = new NotificationDTO();
                    break;
                case ActionType.OpenPage:
                    _currentActionModel = new OpenPageDTO();
                    break;
                case ActionType.CloseTheApplication:
                    _currentActionModel = null;
                    break;
                case ActionType.SendEmail:
                    _currentActionModel = new EmailDTO();
                    break;
                case ActionType.TelsamSendSMS:
                    _currentActionModel = new TelsamSmsDTO();
                    break;
                case ActionType.RunProcess:
                    _currentActionModel = new RunProcessDTO();
                    break;
                case ActionType.AddLineToTextFile:
                    _currentActionModel = new AddLineToTextFileDTO();
                    break;
                case ActionType.OpenWebsiteOnWindow:
                    _currentActionModel = new OpenWebsiteOnWindowDTO();
                    break;
                case ActionType.ShowGoogleMapsDirections:
                    _currentActionModel = new ShowGoogleMapsDirectionDTO();
                    break;
                case ActionType.RunScript:
                    _currentActionModel = new RunScriptDTO();
                    break;
                case ActionType.LogoutUser:
                    _currentActionModel = null;
                    break;
                case ActionType.Wait:
                    _currentActionModel = new WaitDTO();
                    break;
                case ActionType.SendHttpRequest:
                    _currentActionModel = new SendHttpRequestDTO();
                    break;
                default:
                    _currentActionModel = null;
                    break;
            }

            DynamicInputs = DynamicInputGenerator.GenerateInputs(_currentActionModel);
            OnPropertyChanged(nameof(DynamicInputs)); // UI'yi güncellemek için            
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is AppActionDTO appAction)
            {
                AppActionId = appAction.Id;
                AppActionName = appAction.Name;
                AppActionType = appAction.ActionTypeId;
                AppActionProperties = appAction.Properties;
            }
        }

        private void ApplyPropertiesToModel()
        {
            if (_currentActionModel == null || string.IsNullOrEmpty(AppActionProperties))
                return;

            try
            {
                // JSON verisini dinamik tip kullanarak deserialize et
                var modelType = _currentActionModel.GetType();
                _currentActionModel = JsonHelper.Deserialize(AppActionProperties, modelType);

                // Inputları yeniden oluşturup verileri güncelle
                DynamicInputs = DynamicInputGenerator.GenerateInputs(_currentActionModel);
                OnPropertyChanged(nameof(DynamicInputs));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error applying properties: {ex.Message}");
            }
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(AppActionName),
            async () => !string.IsNullOrEmpty(AppActionName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(AppActionName))
        ),
        (
            nameof(AppActionType),
            async () => !string.IsNullOrEmpty(AppActionType.ToString()),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(AppActionType))
        ),         
        (
            nameof(AppActionName),
            async () => !(await _appActionService.GetAllAppActionsAsNoTrackingAsync(x => x.Name == AppActionName)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(AppActionName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            DynamicInputGenerator.UpdateModelFromInputs(_currentActionModel, DynamicInputs);
            AppActionProperties = JsonHelper.Serialize(_currentActionModel);

            var existingAppAction = await _appActionService.GetAppActionByIdAsNoTrackingAsync(AppActionId);
            if (existingAppAction != null)
            {
                existingAppAction.Name = AppActionName;
                existingAppAction.ActionTypeId = AppActionType;
                existingAppAction.Properties = AppActionProperties;

                await _appActionService.UpdateAppActionAsync(existingAppAction);
            }
            else
            {
                var appAction = new AppActionDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = AppActionName,
                    ActionTypeId = AppActionType,
                    Properties = AppActionProperties
                };

                await _appActionService.AddAppActionAsync(appAction);
            }

            GlobalVariables.Navigator.Navigate("AppActions");
        }
    }
}
