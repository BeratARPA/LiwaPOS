using LiwaPOS.WpfAppUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        LoginViewModel _loginViewModel = null;

        public LoginUserControl(LoginViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            _loginViewModel = viewModel;
        }

        private async void PINPad_PINEntered(object sender, string e)
        {
            _loginViewModel.PinCode = e;
            await _loginViewModel.LoginAsync();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
