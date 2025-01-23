using LiwaPOS.BLL.Interfaces;
using LiwaPOS.WpfAppUI.Services;
using LiwaPOS.WpfAppUI.ViewModels.Management;

namespace LiwaPOS.WpfAppUI.Helpers
{
    public class GlobalVariables
    {
        public static Shell? Shell { get; set; } = null;
        public static ManagementViewModel? ManagementViewModel { get; set; } = null;
        public static NavigatorService? Navigator { get; set; } = null;
        public static IServiceProvider? ServiceProvider { get; set; } = null;
        public static ICustomNotificationService? CustomNotificationService { get; set; } = null;

        public static void CloseTab(string header)
        {
            // Global olarak tutulan TabItems koleksiyonundaki ilgili tab'ı bulup kaldıracağız       
            var mainContent = ManagementViewModel;
            if (mainContent != null)
            {
                var tabToClose = mainContent.TabItems.FirstOrDefault(x => x.Header == header);
                if (tabToClose != null)
                {
                    mainContent.TabItems.Remove(tabToClose);
                }
            }
        }
    }
}
