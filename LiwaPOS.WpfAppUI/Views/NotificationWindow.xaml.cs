using LiwaPOS.WpfAppUI.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace LiwaPOS.WpfAppUI.Views
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        NotificationViewModel _viewModel;
        private DispatcherTimer _closeTimer;

        public NotificationWindow(NotificationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void CloseTimer_Tick(object? sender, EventArgs e)
        {
            if (_viewModel.DisplayDurationInSecond != 0)
                _closeTimer.Stop();

            this.Close();
        }

        public void SetDialogResult(bool result)
        {
            if (_viewModel.DisplayDurationInSecond != 0)
                _closeTimer.Stop();

            DialogResult = result;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.DisplayDurationInSecond != 0)
                _closeTimer.Stop();

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as NotificationViewModel;

            if (_viewModel.DisplayDurationInSecond == 0)
                return;

            // Otomatik kapanma için timer ayarı
            _closeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(_viewModel.DisplayDurationInSecond)
            };
            _closeTimer.Tick += CloseTimer_Tick;
            _closeTimer.Start();
        }
    }
}
