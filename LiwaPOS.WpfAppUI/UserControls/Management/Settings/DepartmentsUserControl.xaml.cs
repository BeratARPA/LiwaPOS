using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Settings;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Settings
{
    /// <summary>
    /// Interaction logic for DepartmentsUserControl.xaml
    /// </summary>
    public partial class DepartmentsUserControl : System.Windows.Controls.UserControl
    {
        public DepartmentsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as DepartmentsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("DepartmentManagement", viewModel.SelectedCommand);
            }
        }
    }
}
