using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class AutomationCommandViewModel : ViewModelBase
    {
        private readonly AutomationCommandManager _automationCommandManager;
        private bool _hasExecuted;

        public AutomationCommandDTO _automationCommand { get; }
        public string ButtonHeader => _automationCommand.ButtonHeader ?? _automationCommand.Name;
        public string Color => _automationCommand.Color;
        public int FontSize => _automationCommand.FontSize;
        public bool ToggleValues => _automationCommand.ToggleValues;
        public string ImageSource => _automationCommand.Image;
        public string Symbol => _automationCommand.Symbol;
        public bool ExecuteOnce => _automationCommand.ExecuteOnce;
        public bool ClearSelection => _automationCommand.ClearSelection;
        public int ConfirmationType => _automationCommand.ConfirmationType;
        public string NavigationModule => _automationCommand.NavigationModule;
        public bool AskTextInput => _automationCommand.AskTextInput;
        public bool AskNumericInput => _automationCommand.AskNumericInput;

        public ObservableCollection<string> Values { get; }

        private string _selectedValue;
        public string SelectedValue
        {
            get => _selectedValue;
            set
            {
                if (_selectedValue != value)
                {
                    _selectedValue = value;
                    OnPropertyChanged(nameof(SelectedValue));
                    OnPropertyChanged(nameof(Display));
                }
            }
        }

        public string Display => string.IsNullOrEmpty(SelectedValue) ? ButtonHeader : SelectedValue;

        public AutomationCommandViewModel(AutomationCommandDTO automationCommand, AutomationCommandManager automationCommandManager)
        {
            _automationCommand = automationCommand; 
            _automationCommandManager = automationCommandManager;

            if (_automationCommand.ToggleValues && !string.IsNullOrEmpty(_automationCommand.Values))
            {
                var values = _automationCommand.Values.Split('|');
                Values = new ObservableCollection<string>(values);
                SelectedValue = values.First();
            }

            ExecuteCommand = new AsyncRelayCommand(ExecuteAsync, CanExecute);
        }
       
        public ICommand ExecuteCommand { get; }

        public bool CanExecute(object obj)
        {
            // Burada, komutun etkin olup olmadığını kontrol edebilirsiniz.
            // Örneğin; belirli koşullara göre (ticket durumu, kullanıcı yetkisi vs.) kontrol yapılabilir.
            return true;
        }

        public async Task ExecuteAsync(object obj)
        {
            if (ExecuteOnce && _hasExecuted)
                return;
            
            if (AskTextInput)
            {
                //var textInput = await ShowTextInputPopupAsync();
                // textInput kullanarak işlem yapın
            }

            if (AskNumericInput)
            {
                //var numericInput = await ShowNumericInputPopupAsync();
                // numericInput kullanarak işlem yapın
            }

            if (ConfirmationType == 1) // Onay
            {
                //var isConfirmed = await ShowConfirmationPopupAsync();
                //if (!isConfirmed)
                //    return;
            }

            await _automationCommandManager.ExecuteCommandAsync(_automationCommand, SelectedValue);

            if (ToggleValues && Values.Count > 1)
            {
                var currentIndex = Values.IndexOf(SelectedValue);
                SelectedValue = Values[(currentIndex + 1) % Values.Count];
            }

            _hasExecuted = true;
        }
    }
}
