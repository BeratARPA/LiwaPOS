using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Settings;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Settings
{
    /// <summary>
    /// Interaction logic for TerminalsUserControl.xaml
    /// </summary>
    public partial class TerminalsUserControl : System.Windows.Controls.UserControl
    {
        public TerminalsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as TerminalsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("TerminalManagement", viewModel.SelectedCommand);
            }
        }
    }
}
