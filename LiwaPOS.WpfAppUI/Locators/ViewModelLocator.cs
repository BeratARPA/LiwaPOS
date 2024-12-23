using LiwaPOS.WpfAppUI.Factories;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.General;
using LiwaPOS.WpfAppUI.ViewModels.Management;
using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using LiwaPOS.WpfAppUI.ViewModels.Management.Printing;
using LiwaPOS.WpfAppUI.ViewModels.Management.Settings;
using LiwaPOS.WpfAppUI.ViewModels.Management.Users;

namespace LiwaPOS.WpfAppUI.Locators
{
    public class ViewModelLocator
    {
        private readonly ViewModelFactory _viewModelFactory;

        public ViewModelLocator()
        {
            _viewModelFactory = new ViewModelFactory(GlobalVariables.ServiceProvider);
        }
 
        public AutomationCommandManagementViewModel AutomationCommandManagementViewModel => _viewModelFactory.CreateViewModel<AutomationCommandManagementViewModel>();
        public AutomationCommandsViewModel AutomationCommandsViewModel => _viewModelFactory.CreateViewModel<AutomationCommandsViewModel>();
        public AppActionManagementViewModel AppActionManagementViewModel => _viewModelFactory.CreateViewModel<AppActionManagementViewModel>();
        public AppActionsViewModel AppActionsViewModel => _viewModelFactory.CreateViewModel<AppActionsViewModel>();
        public AppRuleManagementViewModel AppRuleManagementViewModel => _viewModelFactory.CreateViewModel<AppRuleManagementViewModel>();
        public AppRulesViewModel AppRulesViewModel => _viewModelFactory.CreateViewModel<AppRulesViewModel>();
        public ErrorReportViewModel ErrorReportViewModel => _viewModelFactory.CreateViewModel<ErrorReportViewModel>();
        public LoginViewModel LoginViewModel => _viewModelFactory.CreateViewModel<LoginViewModel>();
        public LocalSettingsManagementViewModel LocalSettingsManagementViewModel => _viewModelFactory.CreateViewModel<LocalSettingsManagementViewModel>();
        public ManagementViewModel ManagementViewModel => _viewModelFactory.CreateViewModel<ManagementViewModel>();
        public NavigationViewModel NavigationViewModel => _viewModelFactory.CreateViewModel<NavigationViewModel>();
        public NotificationViewModel NotificationViewModel => _viewModelFactory.CreateViewModel<NotificationViewModel>();
        public ScriptManagementViewModel ScriptManagementViewModel => _viewModelFactory.CreateViewModel<ScriptManagementViewModel>();
        public ScriptsViewModel ScriptsViewModel => _viewModelFactory.CreateViewModel<ScriptsViewModel>();
        public TerminalsViewModel TerminalsViewModel => _viewModelFactory.CreateViewModel<TerminalsViewModel>();
        public TerminalManagementViewModel TerminalManagementViewModel => _viewModelFactory.CreateViewModel<TerminalManagementViewModel>();
        public DepartmentsViewModel DepartmentsViewModel => _viewModelFactory.CreateViewModel<DepartmentsViewModel>();
        public DepartmentManagementViewModel DepartmentManagementViewModel => _viewModelFactory.CreateViewModel<DepartmentManagementViewModel>();
        public PrinterManagementViewModel PrinterManagementViewModel => _viewModelFactory.CreateViewModel<PrinterManagementViewModel>();
        public PrintersViewModel PrintersViewModel => _viewModelFactory.CreateViewModel<PrintersViewModel>();
        public UsersViewModel UsersViewModel => _viewModelFactory.CreateViewModel<UsersViewModel>();
        public UserManagementViewModel UserManagementViewModel => _viewModelFactory.CreateViewModel<UserManagementViewModel>();
        public UserRoleManagementViewModel UserRoleManagementViewModel => _viewModelFactory.CreateViewModel<UserRoleManagementViewModel>();
        public UserRolesViewModel UserRolesViewModel => _viewModelFactory.CreateViewModel<UserRolesViewModel>();
    }
}
