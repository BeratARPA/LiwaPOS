using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Automations
{
    /// <summary>
    /// Interaction logic for AppActionsUserControl.xaml
    /// </summary>
    public partial class AppActionsUserControl : System.Windows.Controls.UserControl
    {
        public AppActionsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as AppActionsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("AppActionManagement", viewModel.SelectedCommand);
            }
        }
    }
}
