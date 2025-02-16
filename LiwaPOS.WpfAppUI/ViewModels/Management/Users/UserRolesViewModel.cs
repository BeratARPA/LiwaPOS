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
    public class UserRolesViewModel : ViewModelBase
    {
        private string _searchText;
        private UserRoleDTO _selectedCommand;
        private ObservableCollection<UserRoleDTO> _commands;
        private ICollectionView _filteredCommands;
        private readonly IUserRoleService _userRoleService;
        private readonly IPermissionService _permissionService;

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

        public UserRoleDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<UserRoleDTO> Commands
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

        public UserRolesViewModel(IUserRoleService userRoleService, IPermissionService permissionService)
        {
            _userRoleService = userRoleService;
            _permissionService = permissionService;

            _ = LoadUserRolesAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
            ReorderCommand = new RelayCommand(ReorderCommands);
        }

        private async Task LoadUserRolesAsync()
        {
            var data = await GetUserRoles();
            Commands = new ObservableCollection<UserRoleDTO>(data);

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
                    var command = obj as UserRoleDTO;
                    return command != null && command.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
            FilteredCommands.Refresh();
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("UserRoleManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("UserRoleManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {               
                await _userRoleService.DeleteUserRoleAsync(SelectedCommand.Id);
                await _permissionService.DeleteAllPermissionsAsync(x => x.UserRoleId == SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<UserRoleDTO>> GetUserRoles()
        {
            return await _userRoleService.GetAllUserRolesAsync();
        }

        // Komutları yeniden sıralama
        private void ReorderCommands(object obj)
        {
            // Örnek: Komutları isme göre sıralama
            Commands = new ObservableCollection<UserRoleDTO>(Commands.OrderBy(c => c.Name));
            FilterCommands();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
