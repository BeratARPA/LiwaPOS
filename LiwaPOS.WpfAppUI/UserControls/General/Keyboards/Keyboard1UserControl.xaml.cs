using LiwaPOS.WpfAppUI.ViewModels.General;

namespace LiwaPOS.WpfAppUI.UserControls.General.Keyboards
{
    /// <summary>
    /// Interaction logic for KeyboardUserControl.xaml
    /// </summary>
    public partial class Keyboard1UserControl : System.Windows.Controls.UserControl
    {
        public Keyboard1UserControl()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null || ((KeyboardViewModel)DataContext).Model == null) return;

            ((KeyboardViewModel)DataContext).Model.ReleaseKey(Keys.ShiftKey);
            ((KeyboardViewModel)DataContext).Model.ReleaseKey(Keys.LShiftKey);
            ((KeyboardViewModel)DataContext).Model.ReleaseKey(Keys.RShiftKey);
        }      
    }
}
