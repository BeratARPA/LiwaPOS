using System.Windows;

namespace LiwaPOS.WpfAppUI.UserControls.General
{
    /// <summary>
    /// Interaction logic for DynamicPropertyEditorWindow.xaml
    /// </summary>
    public partial class DynamicPropertyEditorWindow : Window
    {
        public DynamicPropertyEditorWindow()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
