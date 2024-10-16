using LiwaPOS.WpfAppUI.ViewModels;

namespace LiwaPOS.WpfAppUI.UserControls
{
    /// <summary>
    /// Interaction logic for ManagementUserControl.xaml
    /// </summary>
    public partial class ManagementUserControl : System.Windows.Controls.UserControl
    {
        public ManagementUserControl()
        {
            InitializeComponent();
            
            if(DataContext is ManagementViewModel viewModel)
            {
                viewModel.FrameContent = FrameContent;
            }
        }
    }
}
