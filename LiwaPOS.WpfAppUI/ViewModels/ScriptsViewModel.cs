using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ScriptsViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private ScriptDTO _selectedCommand;
        private ObservableCollection<ScriptDTO> _commands;
        private ObservableCollection<ScriptDTO> _filteredCommands;

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

        public ScriptsViewModel()
        {
            // Örnek komutlar ekleme
            Commands = new ObservableCollection<ScriptDTO>
            {
                new ScriptDTO { Name = "Komut 1" },
                new ScriptDTO { Name = "Komut 2" },
                new ScriptDTO { Name = "Komut 3" }
            };

            FilteredCommands = new ObservableCollection<ScriptDTO>(Commands);

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
            ReorderCommand = new RelayCommand(ReorderCommands);
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
            GlobalVariables.Navigator.Navigate(typeof(ScriptManagementUserControl));
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate(typeof(ScriptManagementUserControl));
            }
        }

        // Seçili komutu silme
        private void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                Commands.Remove(SelectedCommand);
                FilterCommands();
                MessageBox.Show($"{SelectedCommand.Name} komutu silindi.");
            }
        }

        // Komutları yeniden sıralama
        private void ReorderCommands(object obj)
        {
            // Örnek: Komutları isme göre sıralama
            Commands = new ObservableCollection<ScriptDTO>(Commands.OrderBy(c => c.Name));
            FilterCommands();
            MessageBox.Show("Komutlar sıralandı.");
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
