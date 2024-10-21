using LiwaPOS.Shared.Enums;
using System.Windows;

namespace LiwaPOS.WpfAppUI.Converters
{
    internal class VisibilityStateToVisibilityConverter
    {
        public static Visibility Convert(VisibilityState state)
        {
            return state switch
            {
                VisibilityState.Visible => Visibility.Visible,
                VisibilityState.Collapsed => Visibility.Collapsed,
                _ => Visibility.Hidden,
            };
        }
    }
}
