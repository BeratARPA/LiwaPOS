using System.Windows;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for PINPadUserControl.xaml
    /// </summary>
    public partial class PINPadUserControl : System.Windows.Controls.UserControl
    {
        public event EventHandler<string> PINEntered = null;

        private string _pin = null;
        public string PIN
        {
            get { return _pin; }
            set { _pin = value; }
        }


        public PINPadUserControl()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_pin) || PINEntered == null)
                return;

            PINEntered?.Invoke(this, PIN);

            PIN = "";
            TextBoxPIN.Clear();
        }

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            PIN = "";
            TextBoxPIN.Clear();
        }

        private void Numbers(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null)
                return;

            PIN += button.Content;
            TextBoxPIN.Text = PIN;
        }
    }
}
