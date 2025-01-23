using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Users;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Users
{
    /// <summary>
    /// Interaction logic for UsersUserControl.xaml
    /// </summary>
    public partial class UsersUserControl : System.Windows.Controls.UserControl
    {
        public UsersUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as UsersViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("UserManagement", viewModel.SelectedCommand);
            }
        }
    }
}
