using LiwaPOS.BLL.Interfaces;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NavigatorService : INavigatorService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame _frame;

        public NavigatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _frame = GlobalVariables.Shell.FrameContent;
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
                _ => throw new InvalidOperationException($"No view found for {viewName}")
            };

            Navigate(pageType, parameter);
        }

        private void Navigate(Type pageType, object? parameter = null)
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
