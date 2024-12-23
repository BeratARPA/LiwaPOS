using System.Windows;

namespace LiwaPOS.WpfAppUI.Views
{
    /// <summary>
    /// Interaction logic for ErrorReportWindow.xaml
    /// </summary>
    public partial class ErrorReportWindow : Window
    {
        public ErrorReportWindow()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
