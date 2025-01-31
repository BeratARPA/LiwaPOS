using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class AutomationCommandsViewModel : ViewModelBase
    {
        private string _searchText;
        private AutomationCommandDTO _selectedCommand;
        private ObservableCollection<AutomationCommandDTO> _commands;
        private ICollectionView _filteredCommands;
        private readonly IAutomationCommandService _automationCommandService;
        private readonly IAutomationCommandMapService _automationCommandMapService;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterCommands();
            }
        }

        public AutomationCommandDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<AutomationCommandDTO> Commands
        {
            get => _commands;
            set
            {
                _commands = value;
                OnPropertyChanged(nameof(Commands));
            }
        }

        public ICollectionView FilteredCommands
        {
            get => _filteredCommands;
            set
            {
                _filteredCommands = value;
                OnPropertyChanged(nameof(FilteredCommands));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public AutomationCommandsViewModel(IAutomationCommandService automationCommandService, IAutomationCommandMapService automationCommandMapService)
        {
            _automationCommandService = automationCommandService;
            _automationCommandMapService = automationCommandMapService;

            _ = LoadAutomationCommandsAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
        }

        private async Task LoadAutomationCommandsAsync()
        {
            var data = await GetAutomationCommands();
            Commands = new ObservableCollection<AutomationCommandDTO>(data);

            // ICollectionView ile gruplama ve sıralama işlemleri
            FilteredCommands = CollectionViewSource.GetDefaultView(Commands);
            FilteredCommands.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            FilteredCommands.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            FilteredCommands.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        // Arama metni değiştikçe komutları filtreler      
        private void FilterCommands()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCommands.Filter = null;  // Tüm listeyi göster
            }
            else
            {
                FilteredCommands.Filter = obj =>
                {
                    var command = obj as AutomationCommandDTO;
                    return command != null && command.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
            FilteredCommands.Refresh();
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("AutomationCommandManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("AutomationCommandManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _automationCommandService.DeleteAutomationCommandAsync(SelectedCommand.Id);
                await _automationCommandMapService.DeleteAllAutomationCommandMapsAsync(x => x.AutomationCommandId == SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<AutomationCommandDTO>> GetAutomationCommands()
        {
            return await _automationCommandService.GetAllAutomationCommandsAsync();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
