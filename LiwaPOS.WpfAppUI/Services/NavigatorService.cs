using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Managers;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NavigatorService : INavigatorService
    {
        private readonly AppRuleManager _appRuleManager;
        private readonly IServiceProvider _serviceProvider;
        private Frame _frame;

        public NavigatorService(IServiceProvider serviceProvider, AppRuleManager appRuleManager)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _frame = GlobalVariables.Shell?.FrameContent ?? null;
            _appRuleManager = appRuleManager;
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
                "AutomationCommands" => typeof(AutomationCommandsUserControl),
                "AutomationCommandManagement" => typeof(AutomationCommandManagementUserControl),
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
