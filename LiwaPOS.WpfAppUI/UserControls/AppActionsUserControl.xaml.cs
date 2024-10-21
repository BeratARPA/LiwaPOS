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
    /// Interaction logic for AppActionsUserControl.xaml
    /// </summary>
    public partial class AppActionsUserControl : System.Windows.Controls.UserControl
    {
        public AppActionsUserControl()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as AppActionsViewModel;
            if (viewModel?.SelectedCommand != null)
            {
                GlobalVariables.Navigator.Navigate("AppActionManagement", viewModel.SelectedCommand);
            }
        }
    }
}
