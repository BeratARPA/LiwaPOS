using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Interfaces;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace LiwaPOS.WpfAppUI
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private readonly DispatcherTimer _dispatcherTimerTime;
        private readonly IApplicationStateService _applicationState;

        public Shell()
        {
            InitializeComponent();

            _applicationState = GlobalVariables.ServiceProvider.GetRequiredService<IApplicationStateService>();
          
            GlobalVariables.Shell = this;

            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);

            GlobalVariables.Navigator.Navigate(typeof(LoginUserControl));

            _dispatcherTimerTime = new DispatcherTimer();
            _dispatcherTimerTime.Tick += DispatcherTimerTime_Tick;
            TextBlockTime.Text = "...";

            _dispatcherTimerTime.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimerTime.Start();

            var scaleTransform = GridMain.LayoutTransform as ScaleTransform;
            if (scaleTransform != null)
            {
                scaleTransform.ScaleX = Properties.Settings.Default.WindowScale;
                scaleTransform.ScaleY = Properties.Settings.Default.WindowScale;
            }
        }

        private void DispatcherTimerTime_Tick(object? sender, EventArgs e)
        {
            var time = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            TextBlockTime.Text = TextBlockTime.Text.Contains(":") ? time.Replace(":", ".") : time;
        }

        private void TextBlockAppName_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != (ModifierKeys.Control | ModifierKeys.Shift)) return;

            var value = e.Delta / 3000d;

            var scaleTransform = GridMain.LayoutTransform as ScaleTransform;
            if (scaleTransform == null || scaleTransform.ScaleX + value < 0.05) return;

            scaleTransform.ScaleX += value;
            scaleTransform.ScaleY += value;
        }

        private void TextBlockAppName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
                {
                    var scaleTransform = GridMain.LayoutTransform as ScaleTransform;
                    if (scaleTransform != null)
                    {
                        scaleTransform.ScaleX = 1;
                        scaleTransform.ScaleY = 1;
                    }
                    return;
                }

                if (WindowStyle != WindowStyle.SingleBorderWindow)
                {
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Normal;
                }
                else
                {
                    WindowStyle = WindowStyle.None;
                    WindowState = WindowState.Maximized;
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_applicationState.IsLocked)
            {
                e.Cancel = true;
                return;
            }
          
            Properties.Settings.Default.WindowScale = (GridMain.LayoutTransform as ScaleTransform).ScaleX;
            Properties.Settings.Default.Save();
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);
            GlobalVariables.Navigator.Navigate(typeof(NavigationUserControl));
        }
    }
}
