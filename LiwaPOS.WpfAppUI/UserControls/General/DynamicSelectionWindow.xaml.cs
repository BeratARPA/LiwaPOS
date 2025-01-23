using System.Windows;

namespace LiwaPOS.WpfAppUI.UserControls.General
{
    /// <summary>
    /// Interaction logic for DynamicSelectionWindow.xaml
    /// </summary>
    public partial class DynamicSelectionWindow : Window
    {
        public DynamicSelectionWindow()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {           
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
