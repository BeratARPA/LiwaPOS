using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Settings
{
    public class DepartmentsViewModel : ViewModelBase
    {
        private string _searchText;
        private DepartmentDTO _selectedCommand;
        private ObservableCollection<DepartmentDTO> _commands;
        private ObservableCollection<DepartmentDTO> _filteredCommands;
        private readonly IDepartmentService _departmentService;

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

        public DepartmentDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<DepartmentDTO> Commands
        {
            get => _commands;
            set
            {
                _commands = value;
                OnPropertyChanged(nameof(Commands));
            }
        }

        public ObservableCollection<DepartmentDTO> FilteredCommands
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

        public DepartmentsViewModel(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

            _ = LoadDepartmentsAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
            ReorderCommand = new RelayCommand(ReorderCommands);
        }

        private async Task LoadDepartmentsAsync()
        {
            var data = await GetDepartments();
            Commands = new ObservableCollection<DepartmentDTO>(data);
            FilteredCommands = new ObservableCollection<DepartmentDTO>(Commands);
        }

        // Arama metni değiştikçe komutları filtreler
        private void FilterCommands()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCommands = new ObservableCollection<DepartmentDTO>(Commands);
            }
            else
            {
                FilteredCommands = new ObservableCollection<DepartmentDTO>(
                    Commands.Where(c => c.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("DepartmentManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("DepartmentManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _departmentService.DeleteDepartmentAsync(SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return await _departmentService.GetAllDepartmentsAsync();
        }

        // Komutları yeniden sıralama
        private void ReorderCommands(object obj)
        {
            // Örnek: Komutları isme göre sıralama
            Commands = new ObservableCollection<DepartmentDTO>(Commands.OrderBy(c => c.Name));
            FilterCommands();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
