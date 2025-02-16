using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Printing
{
    public class PrinterTemplatesViewModel : ViewModelBase
    {
        private string _searchText;
        private PrinterTemplateDTO _selectedCommand;
        private ObservableCollection<PrinterTemplateDTO> _commands;
        private ICollectionView _filteredCommands;
        private readonly IPrinterTemplateService _printerTemplateService;

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

        public PrinterTemplateDTO SelectedCommand
        {
            get => _selectedCommand;
            set
            {
                _selectedCommand = value;
                OnPropertyChanged(nameof(SelectedCommand));
            }
        }

        public ObservableCollection<PrinterTemplateDTO> Commands
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

        public PrinterTemplatesViewModel(IPrinterTemplateService printerTemplateService)
        {
            _printerTemplateService = printerTemplateService;

            _ = LoadPrinterTemplatesAsync();

            // Komutları tanımlama
            AddCommand = new RelayCommand(AddNewCommand);
            EditCommand = new RelayCommand(EditSelectedCommand, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteSelectedCommand, CanEditOrDelete);
        }

        private async Task LoadPrinterTemplatesAsync()
        {
            var data = await GetPrinterTemplates();
            Commands = new ObservableCollection<PrinterTemplateDTO>(data);

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
                    var command = obj as PrinterTemplateDTO;
                    return command != null && command.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }
            FilteredCommands.Refresh();
        }

        // Yeni komut ekleme
        private void AddNewCommand(object obj)
        {
            GlobalVariables.Navigator.Navigate("PrinterTemplateManagement");
        }

        // Seçili komutu düzenleme
        private void EditSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("PrinterTemplateManagement", SelectedCommand);
            }
        }

        // Seçili komutu silme
        private async void DeleteSelectedCommand(object obj)
        {
            if (SelectedCommand != null)
            {
                await _printerTemplateService.DeletePrinterTemplateAsync(SelectedCommand.Id);
                Commands.Remove(SelectedCommand);
                FilterCommands();
            }
        }

        private async Task<IEnumerable<PrinterTemplateDTO>> GetPrinterTemplates()
        {
            return await _printerTemplateService.GetAllPrinterTemplatesAsync();
        }

        // Komutun düzenlenip silinebilmesi için seçili komut var mı kontrolü
        private bool CanEditOrDelete(object obj)
        {
            return SelectedCommand != null;
        }
    }
}
