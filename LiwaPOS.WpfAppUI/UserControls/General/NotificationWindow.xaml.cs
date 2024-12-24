using LiwaPOS.Shared.Enums;
using LiwaPOS.WpfAppUI.ViewModels.General;
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

            _viewModel.CloseAction = (result) =>
            {
                SetDialogResult(result); // Pencereyi kapatma işlemi
            };

            ChangeColor();
        }

        private void ChangeColor()
        {
            if (_viewModel.Icon == NotificationIcon.Warning)
                BorderOuter.Background = System.Windows.Media.Brushes.Orange;
            else if (_viewModel.Icon == NotificationIcon.Error)
                BorderOuter.Background = System.Windows.Media.Brushes.DarkRed;
            else if (_viewModel.Icon == NotificationIcon.Information)
                BorderOuter.Background = System.Windows.Media.Brushes.DodgerBlue;
            else
                BorderOuter.Background = System.Windows.Media.Brushes.LightSlateGray;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as NotificationViewModel;

            if (_viewModel.DisplayDurationInSecond == 0)
                return;

            if (_viewModel.IsDialog)
                return;

            if (_viewModel.Button != NotificationButtonType.OK && _viewModel.Button != NotificationButtonType.None)
                return;

            // Otomatik kapanma için timer ayarı
            _closeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(_viewModel.DisplayDurationInSecond)
            };
            _closeTimer.Tick += CloseTimer_Tick;
            _closeTimer.Start();
        }

        private void CloseTimer_Tick(object? sender, EventArgs e)
        {
            if (_viewModel.DisplayDurationInSecond != 0)
                _closeTimer.Stop();

            CloseWindow(false);
        }

        public void SetDialogResult(bool result)
        {
            if (_closeTimer != null)
                _closeTimer.Stop();

            CloseWindow(result);
        }

        public void CloseWindow(bool result)
        {
            if (this.IsLoaded && this.IsInitialized && _viewModel.IsDialog) // Pencerenin oluşturulmuş ve dialog modunda olup olmadığını kontrol et
            {
                DialogResult = result;
            }
            else
            {
                this.Close(); // Eğer dialog modunda değilse, sadece pencereyi kapat
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.IsDialog)
                return;

            if (_viewModel.Button != NotificationButtonType.None)
                return;

            if (_viewModel.DisplayDurationInSecond != 0)
                _closeTimer.Stop();

            CloseWindow(false);
        }
    }
}
