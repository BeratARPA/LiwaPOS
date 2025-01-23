using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.Management.Printing
{
    public class PrinterTemplateManagementViewModel : ViewModelBase
    {
        private readonly IPrinterService _printerService;
        private readonly ICustomNotificationService _customNotificationService;
        private string _printerName;
        private string _shareName;
        private bool _rtlMode;
        private string _charReplacement;
        private int _printerId;

        public int PrinterId
        {
            get => _printerId;
            set
            {
                _printerId = value;
                OnPropertyChanged();
            }
        }

        public string PrinterName
        {
            get => _printerName;
            set
            {
                _printerName = value;
                OnPropertyChanged();
            }
        }

        public string ShareName
        {
            get => _shareName;
            set
            {
                _shareName = value;
                OnPropertyChanged();
            }
        }

        public bool RTLMode
        {
            get => _rtlMode;
            set
            {
                _rtlMode = value;
                OnPropertyChanged();
            }
        }

        public string CharReplacement
        {
            get => _charReplacement;
            set
            {
                _charReplacement = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> _windowsPrinters;
        public ObservableCollection<string> WindowsPrinters
        {
            get => _windowsPrinters;
            set
            {
                _windowsPrinters = value;
                OnPropertyChanged(nameof(WindowsPrinters));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public PrinterTemplateManagementViewModel(IPrinterService printerService, ICustomNotificationService customNotificationService)
        {
            _printerService = printerService;
            _customNotificationService = customNotificationService;

            _ = LoadWindowsPrintersAsync();

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
        }

        private async Task LoadWindowsPrintersAsync()
        {
            var data = PrinterHelper.GetPrinters();
            WindowsPrinters = new ObservableCollection<string>(data);
        }

        public void SetParameter(dynamic parameter)
        {
            if (parameter is PrinterDTO printer)
            {
                PrinterId = printer.Id;
                PrinterName = printer.Name;
                ShareName = printer.ShareName;
                RTLMode = printer.RTLMode;
                CharReplacement = printer.CharReplacement;
            }
            else
            {
                // Başka tipte bir veri geldiyse ona göre işlem yapılabilir
            }
        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("PrinterTemplates");
        }

        private async Task<List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>> GetValidationsAsync()
        {
            return new List<(string PropertyName, Func<Task<bool>> ValidationFunction, string Message)>
    {
        (
            nameof(PrinterName),
            async () => !string.IsNullOrEmpty(PrinterName),
            string.Format(await TranslatorExtension.TranslateUI("NotEmpty_ph"), nameof(PrinterName))
        ),
        (
            nameof(PrinterName),
            async () => !(await _printerService.GetAllPrintersAsNoTrackingAsync(x => x.Name == PrinterName&& x.Id != PrinterId)).Any(),
            string.Format(await TranslatorExtension.TranslateUI("AlreadyUse_ph"), nameof(PrinterName))
        )
    };
        }

        private async Task SaveScript(object obj)
        {
            var validations = await GetValidationsAsync();
            var isValid = await ValidateFieldsAsync(validations, _customNotificationService);
            if (!isValid)
                return;

            var existingPrinter = await _printerService.GetPrinterByIdAsNoTrackingAsync(PrinterId);
            if (existingPrinter != null)
            {
                existingPrinter.Name = PrinterName;
                existingPrinter.ShareName = ShareName;
                existingPrinter.RTLMode = RTLMode;
                existingPrinter.CharReplacement = CharReplacement;

                await _printerService.UpdatePrinterAsync(existingPrinter);
            }
            else
            {
                var printer = new PrinterDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = PrinterName,
                    ShareName = ShareName,
                    RTLMode = RTLMode,
                    CharReplacement = CharReplacement
                };

                await _printerService.AddPrinterAsync(printer);
            }

            GlobalVariables.Navigator.Navigate("PrinterTemplates");
        }
    }
}
