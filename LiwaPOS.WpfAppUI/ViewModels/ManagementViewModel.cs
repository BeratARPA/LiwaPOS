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
        public ICommand OpenAppActionsCommand { get; }
        public ICommand OpenAppRulesCommand { get; }
        public Frame FrameContent { get; set; }

        public ManagementViewModel()
        {         
            OpenScriptsCommand = new RelayCommand(OpenScripts);
            OpenAppActionsCommand = new RelayCommand(OpenAppActions);
            OpenAppRulesCommand = new RelayCommand(OpenAppRules);
        }

        private void OpenScripts(object obj)
        {
            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);

            GlobalVariables.Navigator.Navigate(typeof(ScriptsUserControl));
        }

        private void OpenAppActions(object obj)
        {
            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);

            GlobalVariables.Navigator.Navigate(typeof(AppActionsUserControl));
        }

        private void OpenAppRules(object obj)
        {
            GlobalVariables.Navigator = new NavigatorService(FrameContent, GlobalVariables.ServiceProvider);

            GlobalVariables.Navigator.Navigate(typeof(AppRulesUserControl));
        }
    }
}
