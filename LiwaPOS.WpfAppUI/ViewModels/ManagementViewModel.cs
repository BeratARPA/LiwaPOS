using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ManagementViewModel : ViewModelBase
    {
        public ICommand OpenScriptsCommand { get; }
        public Frame FrameContent { get; set; }

        public ManagementViewModel()
        {
            OpenScriptsCommand = new RelayCommand(OpenScripts);
        }

        private void OpenScripts(object obj)
        {
            // NavigatorService'i başlatma
            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);
            GlobalVariables.Navigator.Navigate(typeof(ScriptsUserControl));
        }       
    }
}
