using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Printing;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Printing
{
    /// <summary>
    /// Interaction logic for PrintersUserControl.xaml
    /// </summary>
    public partial class PrintersUserControl : System.Windows.Controls.UserControl
    {
        public PrintersUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as PrintersViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("PrinterManagement", viewModel.SelectedCommand);
            }
        }
    }
}
