using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management;

namespace LiwaPOS.WpfAppUI.UserControls.Management
{
    /// <summary>
    /// Interaction logic for ManagementUserControl.xaml
    /// </summary>
    public partial class ManagementUserControl : System.Windows.Controls.UserControl
    {
        public ManagementUserControl()
        {
            InitializeComponent();
            GlobalVariables.ManagementViewModel = this.DataContext as ManagementViewModel;
        }
    }
}
