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
        public LoginUserControl(LoginViewModel viewModel)
        {
            InitializeComponent();        
            
            DataContext = viewModel;
        }

        private async void PINPad_PINEntered(object sender, string e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.PinCode = e;
                await viewModel.LoginAsync();
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
