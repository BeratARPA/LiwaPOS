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
        NotificationViewModel dataContext;
        private DispatcherTimer _closeTimer;

        public NotificationWindow()
        {
            InitializeComponent();   
        }

        private void CloseTimer_Tick(object? sender, EventArgs e)
        {
            _closeTimer.Stop();
            this.Close();
        }
      
        public void SetDialogResult(bool result)
        {
            _closeTimer.Stop();
            DialogResult = result;
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _closeTimer.Stop();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataContext = DataContext as NotificationViewModel;

            // Otomatik kapanma için timer ayarı
            _closeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(dataContext.DisplayDurationInSecond)
            };
            _closeTimer.Tick += CloseTimer_Tick;
            _closeTimer.Start();
        }
    }
}
