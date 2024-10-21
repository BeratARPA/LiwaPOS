using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.ViewModels;
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
