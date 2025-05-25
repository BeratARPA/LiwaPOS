using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels.Management.Printing;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Printing
{
    /// <summary>
    /// Interaction logic for PrinterTemplatesUserControl.xaml
    /// </summary>
    public partial class PrinterTemplatesUserControl : System.Windows.Controls.UserControl
    {
        public PrinterTemplatesUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = DataContext as PrinterTemplatesViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("PrinterTemplateManagement", viewModel.SelectedCommand);
            }
        }
    }
}
