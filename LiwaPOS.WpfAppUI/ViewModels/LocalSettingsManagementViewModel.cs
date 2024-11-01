using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class LocalSettingsManagementViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public LocalSettingsManagementViewModel()
        {
            
        }

        public void SetParameter(dynamic parameter)
        {

        }

        private async Task ClosePage(object arg)
        {
            GlobalVariables.Navigator.Navigate("AppActions");
        }

        private async Task SaveScript(object obj)
        {
            
        }
    }
}
