using LiwaPOS.WpfAppUI.ViewModels.General;

namespace LiwaPOS.WpfAppUI.UserControls.General.Keyboards
{
    /// <summary>
    /// Interaction logic for Keyboard3UserControl.xaml
    /// </summary>
    public partial class Keyboard3UserControl : System.Windows.Controls.UserControl
    {
        public Keyboard3UserControl()
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
