using LiwaPOS.WpfAppUI.Factories;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels;

namespace LiwaPOS.WpfAppUI.Locators
{
    public class ViewModelLocator
    {
        private readonly ViewModelFactory _viewModelFactory;

        public ViewModelLocator()
        {
            _viewModelFactory = new ViewModelFactory(GlobalVariables.ServiceProvider);
        }

        public ErrorReportViewModel ErrorReportViewModel => _viewModelFactory.CreateViewModel<ErrorReportViewModel>();
        public LoginViewModel LoginViewModel => _viewModelFactory.CreateViewModel<LoginViewModel>();
        public ManagementViewModel ManagementViewModel => _viewModelFactory.CreateViewModel<ManagementViewModel>();
        public NavigationViewModel NavigationViewModel => _viewModelFactory.CreateViewModel<NavigationViewModel>();
        public NotificationViewModel NotificationViewModel => _viewModelFactory.CreateViewModel<NotificationViewModel>();
        public ScriptManagementViewModel ScriptManagementViewModel => _viewModelFactory.CreateViewModel<ScriptManagementViewModel>();
        public ScriptsViewModel ScriptsViewModel => _viewModelFactory.CreateViewModel<ScriptsViewModel>();
    }
}
