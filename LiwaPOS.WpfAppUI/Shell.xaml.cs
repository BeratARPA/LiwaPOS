using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.UserControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace LiwaPOS.WpfAppUI
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public Shell(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            GlobalVariables.Shell = this;
            GlobalVariables.Navigator = new NavigatorService(FrameContent, _serviceProvider);

            GlobalVariables.Navigator.Navigate(typeof(LoginUserControl));
        }

        private void TextBlockAppName_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != (ModifierKeys.Control | ModifierKeys.Shift)) return;

            var val = e.Delta / 3000d;

            var sc = GridMain.LayoutTransform as ScaleTransform;
            if (sc == null || sc.ScaleX + val < 0.05) return;

            sc.ScaleX += val;
            sc.ScaleY += val;
        }

        private void TextBlockAppName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
                {
                    var lt = GridMain.LayoutTransform as ScaleTransform;
                    if (lt != null)
                    {
                        lt.ScaleX = 1;
                        lt.ScaleY = 1;
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
    }
}
