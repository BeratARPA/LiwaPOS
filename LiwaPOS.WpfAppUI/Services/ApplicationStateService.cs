using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Converters;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Services
{
    public class ApplicationStateService : IApplicationStateService
    {     
        private readonly ProgramSettingValueManager _programSettingValueManager;

        public ApplicationStateService(
            ProgramSettingValueManager programSettingValueManager)
        {       
            _programSettingValueManager = programSettingValueManager;
        }

        #region Properties

        private UserDTO _currentLoggedInUser;
        public UserDTO CurrentLoggedInUser
        {
            get { return _currentLoggedInUser ?? (_currentLoggedInUser = GetCurrentUser().Result); }
            set { _currentLoggedInUser = value; }
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        private AppScreenType _activeAppScreen;
        public AppScreenType ActiveAppScreen
        {
            get { return _activeAppScreen; }
            set { _activeAppScreen = value; }
        }

        private TerminalDTO _currentTerminal;
        public TerminalDTO CurrentTerminal
        {
            get { return _currentTerminal ?? (_currentTerminal = GetCurrentTerminal().Result); }
            set { _currentTerminal = value; }
        }

        #endregion

        #region Setters

        public void SetTextBlockUsername()
        {
            GlobalVariables.Shell.TextBlockUsername.Text = CurrentLoggedInUser == null ? "-" : CurrentLoggedInUser.Name ?? "-"; ;
        }

        public void SetGridBottomBarVisibility(VisibilityState visibilityState)
        {
            Visibility visibility = VisibilityStateToVisibilityConverter.Convert(visibilityState);
            GlobalVariables.Shell.GridBottomBar.Visibility = visibility;
        }
      
        public async Task SetCurrentUser(UserDTO? user)
        {
            await SaveSettingAsync("CurrentUser", user?.Name??"");
            _currentLoggedInUser = user;
        }

        public async Task SetCurrentTerminal(string terminalName)
        {
            await SaveSettingAsync("CurrentTerminal", terminalName);
            _currentTerminal = await _programSettingValueManager.GetTerminalByName(terminalName);
        }

        public async Task SetIsLocked(bool value)
        {
            await SaveSettingAsync("IsLocked", value.ToString());
            _isLocked = value;
        }

        public void SetActiveAppScreen(string viewName)
        {          
            ActiveAppScreen = viewName switch
            {
                "Login" => AppScreenType.LoginScreen,
                "Navigation" => AppScreenType.NavigationScreen,
                "WorkPeriods" => AppScreenType.WorkPeriodsScreen,
                "POS" => AppScreenType.POSScreen,
                "Tickets" => AppScreenType.TicketsScreen,
                "Accounts" => AppScreenType.AccountsScreen,
                "Inventories" => AppScreenType.InventoriesScreen,
                "Market" => AppScreenType.MarketScreen,
                "Report" => AppScreenType.ReportScreen,
                "Management" => AppScreenType.LoginScreen,
                _ => AppScreenType.Nothing
            };
        }

        #endregion

        #region Getters

        public async Task<UserDTO> GetCurrentUser()
        {
            var currentUser = await _programSettingValueManager.GetLocalSettingAsync("CurrentUser");
            if (!string.IsNullOrEmpty(currentUser))
            {
                var user = await _programSettingValueManager.GetUserByUserName(currentUser);
                if (user != null) return user;
            }

            return null;
        }

        public async Task<TerminalDTO> GetCurrentTerminal()
        {
            var currentTerminal = await _programSettingValueManager.GetLocalSettingAsync("CurrentTerminal");
            if (!string.IsNullOrEmpty(currentTerminal))
            {
                var terminal = await _programSettingValueManager.GetTerminalByName("CurrentTerminal");
                if (terminal != null) return terminal;
            }

            var defaultTerminal = await _programSettingValueManager.GetDefaultTerminal();
            return defaultTerminal ?? null;
        }

        #endregion      

        private async Task SaveSettingAsync(string key, string value)
        {
            await _programSettingValueManager.SetLocalSettingAsync(key, value);
        }
    }
}
