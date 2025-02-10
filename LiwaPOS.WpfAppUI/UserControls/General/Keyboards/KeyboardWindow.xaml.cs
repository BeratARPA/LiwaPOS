using LiwaPOS.WpfAppUI.Helpers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace LiwaPOS.WpfAppUI.UserControls.General.Keyboards
{
    /// <summary>
    /// Interaction logic for KeyboardWindow.xaml
    /// </summary>
    public partial class KeyboardWindow : Window
    {
        public System.Windows.Controls.TextBox TargetTextBox { get; set; }

        public KeyboardWindow()
        {
            InitializeComponent();

            Top = Properties.Settings.Default.KeyboardTop;
            Left = Properties.Settings.Default.KeyboardLeft;
            Height = Properties.Settings.Default.KeyboardHeight;
            Width = Properties.Settings.Default.KeyboardWidth;

            if (Height <= 0) ResetWindowSize();
            else if ((Top + Height) > SystemParameters.PrimaryScreenHeight) ResetWindowSize();
            else if (Left > System.Windows.Application.Current.MainWindow.Left + System.Windows.Application.Current.MainWindow.Width) ResetWindowSize();
            else if (Left < 0) ResetWindowSize();
        }

        private void SetWindowStyle()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            const int gwlExstyle = (-20);
            const int wsExNoactivate = 0x08000000;
            const int wsExToolWindow = 0x00000080;
            NativeWin32.SetWindowLong(hwnd, gwlExstyle, (IntPtr)(wsExNoactivate | wsExToolWindow));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowStyle();
            var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source != null) source.AddHook(WndProc);
        }

        public void ResetWindowSize()
        {
            if (System.Windows.Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Height = System.Windows.Application.Current.MainWindow.Height / 2;
                Width = System.Windows.Application.Current.MainWindow.Width;
                Top = System.Windows.Application.Current.MainWindow.Top + Height;
                Left = System.Windows.Application.Current.MainWindow.Left;
            }
            else
            {
                Height = SystemParameters.PrimaryScreenHeight / 2;
                Width = SystemParameters.PrimaryScreenWidth;
                Top = (SystemParameters.PrimaryScreenHeight / 2) * 1;
                Left = 0;
            }
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == UnsafeNativeMethods.WM_MOVING || msg == UnsafeNativeMethods.WM_SIZING)
            {
                var m = new Message
                {
                    HWnd = hwnd,
                    Msg = msg,
                    WParam = wParam,
                    LParam = lParam,
                    Result = IntPtr.Zero
                };
                UnsafeNativeMethods.ReDrawWindow(m);
                handled = true;
            }

            if (msg == UnsafeNativeMethods.WM_MOUSEACTIVATE)
            {
                handled = true;
                return new IntPtr(UnsafeNativeMethods.MA_NOACTIVATE);
            }

            return IntPtr.Zero;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.KeyboardHeight = Height;
            Properties.Settings.Default.KeyboardWidth = Width;
            Properties.Settings.Default.KeyboardTop = Top;
            Properties.Settings.Default.KeyboardLeft = Left;
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HideKeyboard();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResetWindowSize();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            TextBoxInput.Focus();
        }

        private Thickness _oldBorderThickness;
        private System.Windows.Media.Brush _oldBorderBrush;

        public void ShowKeyboard()
        {
            TargetTextBox = Keyboard.FocusedElement as System.Windows.Controls.TextBox;
            if (TargetTextBox != null)
            {
                TextBoxInput.Text = TargetTextBox.Text;
                TextBoxInput.SelectionStart = TargetTextBox.SelectionStart;
                TextBoxInput.AcceptsReturn = TargetTextBox.AcceptsReturn;

                _oldBorderThickness = TargetTextBox.BorderThickness;
                TargetTextBox.BorderThickness = new Thickness(2);
                _oldBorderBrush = TargetTextBox.BorderBrush;
                TargetTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            Show();

            if (TargetTextBox != null)
            {
                Keyboard.Focus(TextBoxInput);
                TextBoxInput.SelectAll();
            }
        }

        public void HideKeyboard()
        {
            if (TargetTextBox != null)
            {
                TargetTextBox.Text = TextBoxInput.Text;
                TargetTextBox.SelectionStart = TextBoxInput.SelectionStart;
                TargetTextBox.BorderThickness = _oldBorderThickness;
                TargetTextBox.BorderBrush = _oldBorderBrush;
            }

            TextBoxInput.Text = "";
            TargetTextBox = null;
            Properties.Settings.Default.KeyboardHeight = Height;
            Properties.Settings.Default.KeyboardWidth = Width;
            Properties.Settings.Default.KeyboardTop = Top;
            Properties.Settings.Default.KeyboardLeft = Left;
            Properties.Settings.Default.Save();

            Hide();
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!TextBoxInput.AcceptsReturn && e.Key == Key.Enter)
            {
                e.Handled = true;
                HideKeyboard();
            }

            if (TargetTextBox != null && e.Key == Key.Tab)
            {
                var tb = TargetTextBox;

                e.Handled = true;
                HideKeyboard();
                Keyboard.Focus(tb);
                tb.Focus();
                tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                if (Keyboard.FocusedElement is System.Windows.Controls.TextBox) ShowKeyboard();
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideKeyboard();
        }
    }
}
