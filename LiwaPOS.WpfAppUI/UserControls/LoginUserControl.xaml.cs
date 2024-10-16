using LiwaPOS.WpfAppUI.ViewModels;
using System.Windows;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : System.Windows.Controls.UserControl
    {
        LoginViewModel _loginViewModel = null;

        public LoginUserControl()
        {
            InitializeComponent();

            if (DataContext is LoginViewModel viewModel)
            {
                _loginViewModel = viewModel;
            }
        }

        private async void PINPad_PINEntered(object sender, string e)
        {
            _loginViewModel.PinCode = e;
            await _loginViewModel.LoginAsync();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
