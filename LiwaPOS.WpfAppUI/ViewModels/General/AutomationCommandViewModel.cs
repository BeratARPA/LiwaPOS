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

        public AutomationCommandDTO _automationCommand { get; }
        public string ButtonHeader => _automationCommand.ButtonHeader ?? _automationCommand.Name;
        public string Color => _automationCommand.Color;
        public int FontSize => _automationCommand.FontSize;
        public bool ToggleValues => _automationCommand.ToggleValues;

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
                SelectedValue = values.First();
            }

            ExecuteCommand = new AsyncRelayCommand(ExecuteAsync, CanExecute);
        }

        public void NextValue()
        {
            if (ToggleValues && Values.Count > 1)
            {
                int currentIndex = Values.IndexOf(SelectedValue);
                SelectedValue = Values[(currentIndex + 1) % Values.Count];
            }
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
            await _automationCommandManager.ExecuteCommandAsync(_automationCommand, SelectedValue);
            if (_automationCommand.ToggleValues && !string.IsNullOrEmpty(_automationCommand.Values))
            {
                var values = _automationCommand.Values.Split('|');
                var currentIndex = Array.IndexOf(values, SelectedValue);
                SelectedValue = values[(currentIndex + 1) % values.Length];
            }
        }
    }
}
