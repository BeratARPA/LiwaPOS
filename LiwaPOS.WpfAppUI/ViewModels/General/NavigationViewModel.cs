using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly AutomationCommandManager _automationCommandManager;

        private ObservableCollection<AutomationCommandViewModel> _automationCommands;
        public ObservableCollection<AutomationCommandViewModel> AutomationCommands
        {
            get => _automationCommands;
            set { _automationCommands = value; OnPropertyChanged(); }
        }

        private bool _useCustomNavigation;
        public bool UseCustomNavigation
        {
            get => _useCustomNavigation;
            set
            {
                if (_useCustomNavigation != value)
                {
                    _useCustomNavigation = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand OpenWorkPeriodsCommand { get; }
        public ICommand OpenPOSCommand { get; }
        public ICommand OpenTicketsCommand { get; }
        public ICommand OpenAccountsCommand { get; }
        public ICommand OpenWarehousesCommand { get; }
        public ICommand OpenLiwaMarketCommand { get; }
        public ICommand OpenReportsCommand { get; }
        public ICommand OpenManageCommand { get; }
        public ICommand OpenLogoutCommand { get; }

        public NavigationViewModel(UserManager userManager, AutomationCommandManager automationCommandManager)
        {
            _userManager = userManager;
            _automationCommandManager = automationCommandManager;

            UseCustomNavigation = Properties.Settings.Default.UseCustomNavigation;

            OpenWorkPeriodsCommand = new RelayCommand(OpenWorkPeriods);
            OpenPOSCommand = new RelayCommand(OpenPOS);
            OpenTicketsCommand = new RelayCommand(OpenTickets);
            OpenAccountsCommand = new RelayCommand(OpenAccounts);
            OpenWarehousesCommand = new RelayCommand(OpenWarehouses);
            OpenLiwaMarketCommand = new RelayCommand(OpenLiwaMarket);
            OpenReportsCommand = new RelayCommand(OpenReports);
            OpenManageCommand = new RelayCommand(OpenManage);
            OpenLogoutCommand = new AsyncRelayCommand(OpenLogout);

            _ = LoadAutomationCommands();
        }

        private async Task LoadAutomationCommands()
        {
            if (UseCustomNavigation)
            {
                var automationCommands = await _automationCommandManager.GetAutomationCommands(1, 1, 1, 1, ScreenVisibilityType.DisplayOnNavigation);
                if (automationCommands != null)
                    AutomationCommands = new ObservableCollection<AutomationCommandViewModel>(automationCommands.Select(ac => new AutomationCommandViewModel(ac, _automationCommandManager)));
                else
                    AutomationCommands = new ObservableCollection<AutomationCommandViewModel>();
            }
        }

        private void OpenWorkPeriods(object obj)
        {

        }

        private void OpenPOS(object obj)
        {

        }

        private void OpenTickets(object obj)
        {

        }

        private void OpenAccounts(object obj)
        {

        }

        private void OpenWarehouses(object obj)
        {

        }

        private void OpenLiwaMarket(object obj)
        {

        }

        private void OpenReports(object obj)
        {

        }

        private void OpenManage(object obj)
        {
            GlobalVariables.Navigator.Navigate("Management");
        }

        private async Task OpenLogout(object obj)
        {
            await _userManager.Logout();
        }
    }
}
