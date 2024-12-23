using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Settings
{
    public class TerminalManagementViewModel : ViewModelBase
    {
        private readonly ITerminalService _terminalService;
        private readonly IPrinterService _printerService;
        private readonly ICustomNotificationService _customNotificationService;

        private PrinterDTO _selectedReportPrinter;
        public PrinterDTO SelectedReportPrinter
        {
            get => _selectedReportPrinter;
            set
            {
                _selectedReportPrinter = value;
                _reportPrinterId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        private PrinterDTO _selectedTransactionPrinter;
        public PrinterDTO SelectedTransactionPrinter
        {
            get => _selectedTransactionPrinter;
            set
            {
                _selectedTransactionPrinter = value;
                _transactionPrinterId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PrinterDTO> _printers;
        public ObservableCollection<PrinterDTO> Printers
        {
            get => _printers;
            set
            {
                _printers = value;
                OnPropertyChanged(nameof(Printers));
            }
        }

        private int _terminalId;
        public int TerminalId
        {
            get => _terminalId;
            set
            {
                _terminalId = value;
                OnPropertyChanged();
            }
        }

        private string _terminalName;
        public string TerminalName
        {
            get => _terminalName;
            set
            {
                _terminalName = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefault;
        public bool IsDefault
        {
            get => _isDefault;
            set
            {
                _isDefault = value;
                OnPropertyChanged();
            }
        }

        private int _transactionPrinterId;
        public int ReportPrinterId
        {
            get => _reportPrinterId;
            set
            {
                _reportPrinterId = value;
                OnPropertyChanged();
            }
        }

        private int _reportPrinterId;
        public int TransactionPrinterId
        {
            get => _transactionPrinterId;
            set
            {
                _transactionPrinterId = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public TerminalManagementViewModel(ITerminalService terminalService, IPrinterService printerService, ICustomNotificationService customNotificationService)
        {
            _terminalService = terminalService;
            _printerService = printerService;
            _customNotificationService = customNotificationService;

            _ = LoadPrintersAsync();

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        private async Task LoadPrintersAsync()
        {
            var data = await GetPrinters();
            Printers = new ObservableCollection<PrinterDTO>(data);

            SelectedReportPrinter = Printers.FirstOrDefault(t => t.Id == ReportPrinterId);
            SelectedTransactionPrinter = Printers.FirstOrDefault(t => t.Id == TransactionPrinterId);
        }

        private async Task<IEnumerable<PrinterDTO>> GetPrinters()
        {
            return await _printerService.GetAllPrintersAsync();
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is TerminalDTO terminal)
            {
                TerminalId = terminal.Id;
                TerminalName = terminal.Name;
                IsDefault = terminal.IsDefault;
                ReportPrinterId = terminal.ReportPrinterId;
                TransactionPrinterId = terminal.TransactionPrinterId;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("Terminals");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(TerminalName),
            async () => !string.IsNullOrEmpty(TerminalName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(TerminalName))
        ),       
        (
            nameof(TerminalName),
            async () => !(await _terminalService.GetAllTerminalsAsNoTrackingAsync(x => x.Name == TerminalName)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(TerminalName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var existingTerminal = await _terminalService.GetTerminalByIdAsNoTrackingAsync(TerminalId);
            if (existingTerminal != null)
            {
                existingTerminal.Name = TerminalName;
                existingTerminal.IsDefault = IsDefault;
                existingTerminal.ReportPrinterId = ReportPrinterId;
                existingTerminal.TransactionPrinterId = TransactionPrinterId;

                await _terminalService.UpdateTerminalAsync(existingTerminal);
            }
            else
            {
                var department = new TerminalDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = TerminalName,
                    IsDefault = IsDefault,
                    ReportPrinterId = ReportPrinterId,
                    TransactionPrinterId = TransactionPrinterId
                };

                await _terminalService.AddTerminalAsync(department);
            }

            GlobalVariables.Navigator.Navigate("Terminals");
        }
    }
}
