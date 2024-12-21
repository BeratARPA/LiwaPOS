using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class TerminalManagementViewModel : ViewModelBase
    {
        private readonly ITerminalService _terminalService;
        private string _terminalName;
        private bool _isDefault;
        private int _reportPrinterId;
        private int _transactionPrinterId;
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

        public string TerminalName
        {
            get => _terminalName;
            set
            {
                _terminalName = value;
                OnPropertyChanged();
            }
        }

        public bool IsDefault
        {
            get => _isDefault;
            set
            {
                _isDefault = value;
                OnPropertyChanged();
            }
        }

        public int ReportPrinterId
        {
            get => _reportPrinterId;
            set
            {
                _reportPrinterId = value;
                OnPropertyChanged();
            }
        }

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

        public TerminalManagementViewModel(ITerminalService terminalService)
        {
            _terminalService = terminalService;

            SaveCommand = new AsyncRelayCommand(SaveScript);
            CloseCommand = new AsyncRelayCommand(ClosePage);
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

        private async Task SaveScript(object obj)
        {
            if (string.IsNullOrEmpty(TerminalName))
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
