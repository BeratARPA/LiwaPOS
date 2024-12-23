using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Automation
{
    public class ScriptsViewModel : ViewModelBase
    {
        private string _searchText;
        private ScriptDTO _selectedCommand;
        private ObservableCollection<ScriptDTO> _commands;
        private ObservableCollection<ScriptDTO> _filteredCommands;
        private readonly IScriptService _scriptService;

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

        public ScriptDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<ScriptDTO> Commands
        {
            get => _commands;
            set
            {
                _commands = value;
                OnPropertyChanged(nameof(Commands));
            }
        }

        public ObservableCollection<ScriptDTO> FilteredCommands
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
        public ICommand ReorderCommand { get; }

        public ScriptsViewModel(IScriptService scriptService)
        {
            _scriptService = scriptService;

            _ = LoadScriptsAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
            ReorderCommand = new RelayCommand(ReorderCommands);
        }

        private async Task LoadScriptsAsync()
        {
            var data = await GetScripts();
            Commands = new ObservableCollection<ScriptDTO>(data);
            FilteredCommands = new ObservableCollection<ScriptDTO>(Commands);
        }

        // Arama metni değiştikçe komutları filtreler
        private void FilterCommands()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCommands = new ObservableCollection<ScriptDTO>(Commands);
            }
            else
            {
                FilteredCommands = new ObservableCollection<ScriptDTO>(
                    Commands.Where(c => c.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("ScriptManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("ScriptManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _scriptService.DeleteScriptAsync(SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<ScriptDTO>> GetScripts()
        {
            return await _scriptService.GetAllScriptsAsync();
        }

        // Komutları yeniden sıralama
        private void ReorderCommands(object obj)
        {
            // Örnek: Komutları isme göre sıralama
            Commands = new ObservableCollection<ScriptDTO>(Commands.OrderBy(c => c.Name));
            FilterCommands();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
