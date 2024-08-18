using System.Windows;
using System.Windows.Controls;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for PINPadUserControl.xaml
    /// </summary>
    public partial class PINPadUserControl : UserControl
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
        }

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            PIN = "";
            TextBoxPIN.Clear();
        }

        private void Numbers(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            PIN += button.Content;
            TextBoxPIN.Text = PIN;
        }
    }
}
