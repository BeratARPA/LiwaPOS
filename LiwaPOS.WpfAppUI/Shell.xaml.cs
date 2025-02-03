using LiwaPOS.BLL.Interfaces;
using LiwaPOS.WpfAppUI.Helpers;
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
        private readonly IDepartmentService _departmentService;
        private readonly IApplicationStateService _applicationStateService;

        public Shell()
        {
            InitializeComponent();

            _applicationStateService = GlobalVariables.ServiceProvider.GetRequiredService<IApplicationStateService>();
            _departmentService = GlobalVariables.ServiceProvider.GetRequiredService<IDepartmentService>();

            GlobalVariables.Shell = this;
            
            GlobalVariables.Navigator.SetFrame(FrameContent);
            GlobalVariables.Navigator.Navigate("Login");

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

            _ = UpdateDepartmentButtonsVisibilityAsync();
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

        private async Task UpdateDepartmentButtonsVisibilityAsync()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();

            if (departments.Count() > 1)
            {
                StackPanelButtons.Visibility = Visibility.Visible;

                // Mevcut butonları temizle
                StackPanelButtons.Children.Clear();

                foreach (var department in departments)
                {
                    var button = new System.Windows.Controls.Button
                    {
                        Content = department.Name,
                        Margin = new Thickness(0, 5, 10, 5),
                        Tag = department.Id // İleride kullanılabilir
                    };

                    button.Click += ButtonDepartment_Click;
                    StackPanelButtons.Children.Add(button);
                }
            }
            else
            {
                StackPanelButtons.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button && button.Tag is int departmentId)
            {
                // Departman ID'sine göre işlemleri yapabilirsiniz
                System.Windows.MessageBox.Show($"Departman seçildi: {button.Content} (ID: {departmentId})");
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_applicationStateService.IsLocked)
            {
                e.Cancel = true;
                return;
            }

            Properties.Settings.Default.WindowScale = (GridMain.LayoutTransform as ScaleTransform).ScaleX;
            Properties.Settings.Default.Save();
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.Navigator.SetFrame(FrameContent);
            GlobalVariables.Navigator.Navigate("Navigation");
        }

        private void ButtonKeyboard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
