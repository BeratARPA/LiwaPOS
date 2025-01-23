using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls.General;
using LiwaPOS.WpfAppUI.UserControls.Management;
using LiwaPOS.WpfAppUI.UserControls.Management.Automations;
using LiwaPOS.WpfAppUI.UserControls.Management.Printing;
using LiwaPOS.WpfAppUI.UserControls.Management.Settings;
using LiwaPOS.WpfAppUI.UserControls.Management.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NavigatorService : INavigatorService
    {
        private readonly AppRuleManager _appRuleManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IApplicationStateService _applicationStateService;
        private Frame _frame;

        public NavigatorService(IServiceProvider serviceProvider, IApplicationStateService applicationStateService, AppRuleManager appRuleManager)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _frame = GlobalVariables.Shell?.FrameContent ?? null;
            _appRuleManager = appRuleManager;
            _applicationStateService = applicationStateService;
        }

        public void SetFrame(Frame frame)
        {
            _frame = frame ?? GlobalVariables.Shell.FrameContent;
        }

        public void Navigate(string viewName, object? parameter = null)
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Frame has not been set in NavigatorService.");
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                _frame.Content = null;
                return;
            }

            Type pageType = viewName switch
            {
                "AppActionManagement" => typeof(AppActionManagementUserControl),
                "AppActions" => typeof(AppActionsUserControl),
                "AppRuleManagement" => typeof(AppRuleManagementUserControl),
                "AppRules" => typeof(AppRulesUserControl),
                "Login" => typeof(LoginUserControl),
                "Management" => typeof(ManagementUserControl),
                "Navigation" => typeof(NavigationUserControl),
                "PINPad" => typeof(PINPadUserControl),
                "ScriptManagement" => typeof(ScriptManagementUserControl),
                "Scripts" => typeof(ScriptsUserControl),
                "Printers" => typeof(PrintersUserControl),
                "PrinterTemplates" => typeof(PrinterTemplatesUserControl),
                "PrinterManagement" => typeof(PrinterManagementUserControl),
                "PrinterTemplateManagement" => typeof(PrinterTemplateManagementUserControl),
                "Terminals" => typeof(TerminalsUserControl),
                "Users" => typeof(UsersUserControl),
                "UserManagement" => typeof(UserManagementUserControl),
                "UserRoles" => typeof(UserRolesUserControl),
                "UserRoleManagement" => typeof(UserRoleManagementUserControl),
                "TerminalManagement" => typeof(TerminalManagementUserControl),
                "Departments" => typeof(DepartmentsUserControl),
                "DepartmentManagement" => typeof(DepartmentManagementUserControl),
                "AutomationCommands" => typeof(AutomationCommandsUserControl),
                "AutomationCommandManagement" => typeof(AutomationCommandManagementUserControl),
                "LocalSettingsManagement" => typeof(LocalSettingsManagementUserControl),
                _ => throw new InvalidOperationException($"No view found for {viewName}")
            };

            Navigate(pageType, parameter, viewName);
        }

        private async Task Navigate(Type pageType, object? parameter = null, string viewName = null)
        {
            var page = _serviceProvider.GetRequiredService(pageType) as System.Windows.Controls.UserControl;
            if (page == null)
            {
                throw new InvalidOperationException($"Service for type '{pageType}' not registered.");
            }

            if (parameter != null)
            {
                var viewModel = page.DataContext as dynamic;
                viewModel?.SetParameter(parameter);
            }

            _frame.Navigate(page);

            using (var scope = _serviceProvider.CreateScope())
            {
                var appRuleManager = scope.ServiceProvider.GetRequiredService<AppRuleManager>();
                await appRuleManager.ExecuteAppRulesForEventAsync(EventType.PageOpened, new PageOpenedDTO { ViewName = viewName });
            }

            _applicationStateService.ActiveAppScreen = viewName switch
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

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }

        public void GoForward()
        {
            if (_frame.CanGoForward)
            {
                _frame.GoForward();
            }
        }
    }
}
