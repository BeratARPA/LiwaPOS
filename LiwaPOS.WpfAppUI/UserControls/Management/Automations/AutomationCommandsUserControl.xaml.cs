using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Automations
{
    /// <summary>
    /// Interaction logic for AutomationCommandsUserControl.xaml
    /// </summary>
    public partial class AutomationCommandsUserControl : System.Windows.Controls.UserControl
    {
        public AutomationCommandsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as AutomationCommandsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("AutomationCommandManagement", viewModel.SelectedCommand);
            }
        }
    }
}
