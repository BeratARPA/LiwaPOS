using LiwaPOS.WpfAppUI.ViewModels.Management.Printing;

namespace LiwaPOS.WpfAppUI.UserControls.Management.Printing
{
    /// <summary>
    /// Interaction logic for PrinterTemplateManagementUserControl.xaml
    /// </summary>
    public partial class PrinterTemplateManagementUserControl : System.Windows.Controls.UserControl
    {
        public PrinterTemplateManagementUserControl()
        {
            InitializeComponent();
            if (DataContext is PrinterTemplateManagementViewModel viewModel)
            {
                viewModel.SetWebView(MonacoEditorWebView);
            }
        }
    }
}
