using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Automations
{
    /// <summary>
    /// Interaction logic for AppRulesUserControl.xaml
    /// </summary>
    public partial class AppRulesUserControl : System.Windows.Controls.UserControl
    {
        public AppRulesUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as AppRulesViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("AppRuleManagement", viewModel.SelectedCommand);
            }
        }
    }
}
