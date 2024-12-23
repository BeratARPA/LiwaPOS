using LiwaPOS.WpfAppUI.ViewModels.Management.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiwaPOS.WpfAppUI.UserControls
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
