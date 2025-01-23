using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Automations
{
    /// <summary>
    /// Interaction logic for ScriptsUserControl.xaml
    /// </summary>
    public partial class ScriptsUserControl : System.Windows.Controls.UserControl
    {
        public ScriptsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as ScriptsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("ScriptManagement", viewModel.SelectedCommand);
            }
        }
    }
}
