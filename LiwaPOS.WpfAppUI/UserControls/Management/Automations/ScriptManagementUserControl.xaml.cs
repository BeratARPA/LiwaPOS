using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Automations
{
    /// <summary>
    /// Interaction logic for ScriptManagementUserControl.xaml
    /// </summary>
    public partial class ScriptManagementUserControl : System.Windows.Controls.UserControl
    {
        public ScriptManagementUserControl()
        {
            InitializeComponent();
            if (DataContext is ScriptManagementViewModel viewModel)
            {
                viewModel.SetWebView(MonacoEditorWebView);
            }
        }
    }
}
