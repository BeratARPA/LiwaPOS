using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Users
{
    public class UsersViewModel : ViewModelBase
    {
        private string _searchText;
        private UserDTO _selectedCommand;
        private ObservableCollection<UserDTO> _commands;
        private ICollectionView _filteredCommands;
        private readonly IUserService _userService;

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

        public UserDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<UserDTO> Commands
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
        public ICommand ReorderCommand { get; }

        public UsersViewModel(IUserService userService)
        {
            _userService = userService;

            _ = LoadUsersAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
            ReorderCommand = new RelayCommand(ReorderCommands);
        }

        private async Task LoadUsersAsync()
        {
            var data = await GetUsers();
            Commands = new ObservableCollection<UserDTO>(data);

            // ICollectionView ile gruplama ve sıralama işlemleri
            FilteredCommands = CollectionViewSource.GetDefaultView(Commands);
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
                    var command = obj as UserDTO;
                    return command != null && command.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
            FilteredCommands.Refresh();
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("UserManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("UserManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _userService.DeleteUserAsync(SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        // Komutları yeniden sıralama
        private void ReorderCommands(object obj)
        {
            // Örnek: Komutları isme göre sıralama
            Commands = new ObservableCollection<UserDTO>(Commands.OrderBy(c => c.Name));
            FilterCommands();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
