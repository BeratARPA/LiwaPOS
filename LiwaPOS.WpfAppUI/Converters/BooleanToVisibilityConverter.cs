using LiwaPOS.Shared.Services;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LiwaPOS.WpfAppUI.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                // Boolean değer true ise Visibility.Visible, false ise Visibility.Collapsed döner
                return booleanValue ? Visibility.Visible : Visibility.Collapsed;
            }

            // Boolean değilse, dönüş için varsayılan değer döner
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // İki yönlü veri bağlaması için genellikle kullanılmaz
            LoggingService.LogErrorAsync("", typeof(BooleanToVisibilityConverter).Name, "", new NotImplementedException());
            return null;
        }
    }
}
