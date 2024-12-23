using LiwaPOS.WpfAppUI.Helpers;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for NavigationUserControl.xaml
    /// </summary>
    public partial class NavigationUserControl : System.Windows.Controls.UserControl
    {
        public NavigationUserControl()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            GlobalVariables.Navigator.Navigate("Management");
        }
    }
}
