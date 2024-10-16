using LiwaPOS.BLL.Interfaces;
using LiwaPOS.WpfAppUI.Services;

namespace LiwaPOS.WpfAppUI.Helpers
{
    public class GlobalVariables
    {
        public static Shell? Shell { get; set; } = null;
        public static NavigatorService? Navigator { get; set; } = null;
        public static IServiceProvider? ServiceProvider { get; set; } = null;
        public static ICustomNotificationService? CustomNotificationService { get; set; } = null;
    }
}
