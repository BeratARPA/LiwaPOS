using LiwaPOS.BLL.Helpers;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using LiwaPOS.WpfAppUI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class AppRuleManagementViewModel : ViewModelBase
    {
        private readonly IAppRuleService _appRuleService;
        private readonly IAppActionService _appActionService;
        private readonly IRuleActionMapService _ruleActionMapService;

        private int _appRuleId;
        private string _appRuleName;
        private EventType _appRuleType;

        public ObservableCollection<EventType> EventTypes { get; }
        public ObservableCollection<AppActionDTO> SelectedActions { get; set; }

        private ConditionMatchType _selectedConditionMatchType;
        public ObservableCollection<ConditionMatchType> ConditionMatchTypes { get; }
        public ObservableCollection<ConstraintItemViewModel> Constraints { get; set; }
        public ObservableCollection<string> DataObjectProperties { get; set; }

        public ConditionMatchType SelectedConditionMatchType
        {
            get => _selectedConditionMatchType;
            set
            {
                _selectedConditionMatchType = value;
                OnPropertyChanged();
            }
        }

        public int AppRuleId
        {
            get => _appRuleId;
            set
            {
                _appRuleId = value;
                OnPropertyChanged();
            }
        }

        public string AppRuleName
        {
            get => _appRuleName;
            set
            {
                _appRuleName = value;
                OnPropertyChanged();
            }
        }

        public EventType AppRuleType
        {
            get => _appRuleType;
            set
            {
                _appRuleType = value;
                OnPropertyChanged();
                UpdateDataObjectProperties();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand OpenActionSelectionCommand { get; }
        public ICommand AddConstraintCommand { get; }
        public ICommand RemoveConstraintCommand { get; }

        public AppRuleManagementViewModel(IAppRuleService appRuleService, IAppActionService appActionService, IRuleActionMapService ruleActionMapService)
        {
            _appRuleService = appRuleService;
            _appActionService = appActionService;
            _ruleActionMapService = ruleActionMapService;

            EventTypes = new ObservableCollection<EventType>(Enum.GetValues(typeof(EventType)).Cast<EventType>());
            SelectedActions = new ObservableCollection<AppActionDTO>();            
            Constraints = new ObservableCollection<ConstraintItemViewModel>();
            ConditionMatchTypes = new ObservableCollection<ConditionMatchType>(Enum.GetValues(typeof(ConditionMatchType)).Cast<ConditionMatchType>());
            DataObjectProperties = new ObservableCollection<string>();

            SaveCommand = new AsyncRelayCommand(SaveScript);
            OpenActionSelectionCommand = new RelayCommand(OpenActionSelectionWindow);
            AddConstraintCommand = new RelayCommand(AddConstraint);
            RemoveConstraintCommand = new RelayCommand(RemoveConstraint);

            UpdateDataObjectProperties();
        }

        private void UpdateDataObjectProperties()
        {
            // EventConfig'den event metadata'larını alıyoruz
            var eventMetadata = EventConfig.GetEventMetadata()
                .FirstOrDefault(em => em.EventType == AppRuleType);

            if (eventMetadata != null && eventMetadata.DataObjectType != null)
            {
                // DataObject özelliklerini alıyoruz
                var properties = PropertyHelper.GetDataObjectProperties(eventMetadata.DataObjectType);
                DataObjectProperties.Clear();
                foreach (var property in properties)
                {
                    DataObjectProperties.Add(property);  // Özellikleri DataObjectProperties listesine ekliyoruz
                }

                return;
            }

            DataObjectProperties.Clear();
        }

        private void AddConstraint(object obj)
        {
            if (!string.IsNullOrEmpty(SelectedConditionMatchType.ToString()))
            {
                // Dinamik olarak eklenen kısıtlamayı yapılandırıyoruz.
                var newConstraint = new ConstraintItemViewModel
                {
                    DataObject = DataObjectProperties, // Özellikleri burada kullanıyoruz
                    Operations = new ObservableCollection<string>(Operations.AllOperations),
                    SelectedValue = null,
                    SelectedOperation = null,
                    Right = string.Empty
                };

                Constraints.Add(newConstraint);
            }
        }

        private void RemoveConstraint(object obj)
        {
            if (obj is ConstraintItemViewModel constraint)
            {
                Constraints.Remove(constraint);
            }
        }

        private async void OpenActionSelectionWindow(object obj)
        {
            // Eylem seçimi için dinamik pencereyi aç
            var availableActions = new ObservableCollection<AppActionDTO>();
            var actions = await _appActionService.GetAllAppActionsAsync();
            foreach (var action in actions)
            {
                availableActions.Add(action);
            }

            var dynamicSelectionViewModel = new DynamicSelectionViewModel<AppActionDTO>(availableActions, SelectedActions, OnActionsSelected);
            dynamicSelectionViewModel.Title = await TranslatorExtension.Translate("SelectActions");
            var actionSelectionWindow = new DynamicSelectionWindow { DataContext = dynamicSelectionViewModel };
            actionSelectionWindow.ShowDialog();
        }

        private void OnActionsSelected(ObservableCollection<AppActionDTO> selectedItems)
        {
            // Seçilen eylemleri güncelle
            SelectedActions = selectedItems;
        }

        public async void SetParameter(dynamic parameter)
        {
            if (parameter is AppRuleDTO appRule)
            {
                AppRuleId = appRule.Id;
                AppRuleName = appRule.Name;
                SelectedConditionMatchType = appRule.ConditionMatch;
                AppRuleType = appRule.Type;

                // Constraints JSON verisini deseralize ediyoruz.
                if (!string.IsNullOrEmpty(appRule.Constraints))
                {                   
                    var ruleConstraints = JsonHelper.Deserialize<List<RuleConstraintDTO>>(appRule.Constraints);

                    // Deseralize edilmiş verileri ConstraintItemViewModel'e dönüştürüp ObservableCollection'a ekliyoruz.
                    Constraints.Clear();  // Eski kısıtlamaları temizle
                    UpdateDataObjectProperties();
                    foreach (var ruleConstraint in ruleConstraints)
                    {
                        Constraints.Add(new ConstraintItemViewModel
                        {
                            DataObject = DataObjectProperties, // Özellikleri burada kullanıyoruz
                            Operations = new ObservableCollection<string>(Operations.AllOperations),
                            SelectedValue = ruleConstraint.Left,  // Burada, Property ismi olarak "Name" kullanılıyor
                            SelectedOperation = ruleConstraint.Operation,
                            Right = ruleConstraint.Right,
                            // Eğer gerekli başka özellikler varsa onları da ekleyebilirsiniz
                        });
                    }
                }

                // Eylem haritalarını alıyoruz
                var ruleActionMaps = await _ruleActionMapService.GetAllRuleActionMapsAsNoTrackingAsync(x => x.AppRuleId == appRule.Id);
                foreach (var ruleActionMap in ruleActionMaps)
                {
                    var action = await _appActionService.GetAppActionAsync(x => x.Id == ruleActionMap.AppActionId);
                    if (action != null)
                    {
                        SelectedActions.Add(action);
                    }
                }
            }
        }

        public string ConvertRuleConstraintsToJson(List<RuleConstraintDTO> ruleConstraints)
        {
            var json = ruleConstraints
                .Where(ruleConstraint =>
                    !string.IsNullOrEmpty(ruleConstraint.Left) &&
                    !string.IsNullOrEmpty(ruleConstraint.Operation) &&
                    !string.IsNullOrEmpty(ruleConstraint.Right)) // Boş olmayanları filtrele
                .Select(ruleConstraint => new
                {
                    Left = ruleConstraint.Left,
                    Name = ruleConstraint.Name,
                    Operation = ruleConstraint.Operation,
                    Right = ruleConstraint.Right
                }).ToList();

            return JsonHelper.Serialize(json);
        }

        private async Task SaveScript(object obj)
        {
            if (string.IsNullOrEmpty(AppRuleName))
                return;

            if (string.IsNullOrEmpty(AppRuleType.ToString()))
                return;

            if (SelectedActions == null)
                return;
            if (SelectedActions.Count <= 0)
                return;

            string constraints = ConvertRuleConstraintsToJson(Constraints.Select(x => 
            new RuleConstraintDTO
            {
                Name = Utility.RandomString(9),
                Left = x.SelectedValue,
                Operation = x.SelectedOperation,
                Right = x.Right
            }).ToList());

            var existingAppRule = await _appRuleService.GetAppRuleByIdAsNoTrackingAsync(AppRuleId);
            if (existingAppRule != null)
            {
                existingAppRule.Name = AppRuleName;
                existingAppRule.ConditionMatch = SelectedConditionMatchType;
                existingAppRule.Constraints = constraints;
                existingAppRule.Type = AppRuleType;

                await _appRuleService.UpdateAppRuleAsync(existingAppRule);

                var existingRuleActionMaps = await _ruleActionMapService.GetAllRuleActionMapsAsNoTrackingAsync(x => x.AppRuleId == existingAppRule.Id);
                foreach (var existingRuleActionMap in existingRuleActionMaps)
                {
                    await _ruleActionMapService.DeleteRuleActionMapAsync(existingRuleActionMap.Id);
                }

                foreach (var action in SelectedActions)
                {
                    var ruleActionMap = new RuleActionMapDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        AppRuleId = existingAppRule.Id,
                        AppActionId = action.Id
                    };

                    await _ruleActionMapService.AddRuleActionMapAsync(ruleActionMap);
                }
            }
            else
            {
                var appRule = new AppRuleDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = AppRuleName,
                    ConditionMatch = SelectedConditionMatchType,
                    Constraints = constraints,
                    Type = AppRuleType,
                };

                await _appRuleService.AddAppRuleAsync(appRule);

                var newAppRule = await _appRuleService.GetAppRuleAsNoTrackingAsync(x => x.EntityGuid == appRule.EntityGuid);

                foreach (var action in SelectedActions)
                {
                    var ruleActionMap = new RuleActionMapDTO
                    {
                        EntityGuid = Guid.NewGuid(),
                        AppRuleId = newAppRule.Id,
                        AppActionId = action.Id
                    };

                    await _ruleActionMapService.AddRuleActionMapAsync(ruleActionMap);
                }
            }

            GlobalVariables.Navigator.Navigate("AppRules");
        }
    }
}
