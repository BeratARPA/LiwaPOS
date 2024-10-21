using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Helpers;
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
            GlobalVariables.Navigator.SetFrame(FrameContent);
            GlobalVariables.Navigator.Navigate("Scripts");
        }

        private void OpenAppActions(object obj)
        {
            GlobalVariables.Navigator.SetFrame(FrameContent);
            GlobalVariables.Navigator.Navigate("AppActions");
        }

        private void OpenAppRules(object obj)
        {
            GlobalVariables.Navigator.SetFrame(FrameContent);
            GlobalVariables.Navigator.Navigate("AppRules");
        }
    }
}
