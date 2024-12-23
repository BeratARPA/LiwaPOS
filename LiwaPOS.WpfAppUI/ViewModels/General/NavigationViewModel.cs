using LiwaPOS.BLL.Managers;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels.General
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;

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

        public NavigationViewModel(UserManager userManager)
        {
            _userManager = userManager;
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
