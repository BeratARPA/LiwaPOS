using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Users;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Users
{
    /// <summary>
    /// Interaction logic for UserRolesUserControl.xaml
    /// </summary>
    public partial class UserRolesUserControl : System.Windows.Controls.UserControl
    {
        public UserRolesUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as UserRolesViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("UserRoleManagement", viewModel.SelectedCommand);
            }
        }
    }
}
