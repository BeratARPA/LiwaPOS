using LiwaPOS.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.Services
{
    public class NavigatorService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame _frame;

        public NavigatorService(Frame frame, IServiceProvider serviceProvider)
        {
            if (frame == null || serviceProvider == null)
            {
                LoggingService.LogErrorAsync("", typeof(NavigatorService).Name, "", new ArgumentNullException());
                return;
            }
            _frame = frame;
            _serviceProvider = serviceProvider;
        }

        public void Navigate(Type pageType, object parameter = null)
        {
            if (_frame == null)
            {
                LoggingService.LogErrorAsync("Frame is not initialized.", typeof(NavigatorService).Name, _frame.ToString(), new InvalidOperationException());
                return;
            }

            var page = _serviceProvider.GetRequiredService(pageType) as System.Windows.Controls.UserControl;
            if (page == null)
            {
                LoggingService.LogErrorAsync($"No service for type '{pageType}' has been registered.", typeof(NavigatorService).Name, pageType.ToString(), new InvalidOperationException());
                return;
            }

            var viewModel = page.DataContext as dynamic;
            if (viewModel != null)
            {
                viewModel.SetParameter(parameter);
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

        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }
    }
}
