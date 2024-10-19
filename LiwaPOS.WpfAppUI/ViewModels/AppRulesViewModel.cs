using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class AppRulesViewModel : ViewModelBase
    {
        private string _searchText;
        private AppRuleDTO _selectedCommand;
        private ObservableCollection<AppRuleDTO> _commands;
        private ICollectionView _filteredCommands;
        private readonly IAppRuleService _appRuleService;

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

        public AppRuleDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<AppRuleDTO> Commands
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

        public AppRulesViewModel(IAppRuleService appRuleService)
        {
            _appRuleService = appRuleService;

            LoadAppRulesAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
        }

        private async void LoadAppRulesAsync()
        {
            var appActions = await GetAppRules();
            Commands = new ObservableCollection<AppRuleDTO>(appActions);

            // ICollectionView ile gruplama ve sıralama işlemleri
            FilteredCommands = CollectionViewSource.GetDefaultView(Commands);
            FilteredCommands.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
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
                    var command = obj as AppRuleDTO;
                    return command != null && command.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
            FilteredCommands.Refresh();
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate(typeof(AppRuleManagementUserControl));
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate(typeof(AppRuleManagementUserControl), SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _appRuleService.DeleteAppRuleAsync(SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<AppRuleDTO>> GetAppRules()
        {
            return await _appRuleService.GetAllAppRulesAsync();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
