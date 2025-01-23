using System.Windows;
using System.Windows.Media;

namespace LiwaPOS.WpfAppUI.Themes
{
    public static class ThemesController {
        public static ThemeType CurrentTheme { get; set; }

        private static ResourceDictionary ThemeDictionary {
            get => System.Windows.Application.Current.Resources.MergedDictionaries[0];
            set => System.Windows.Application.Current.Resources.MergedDictionaries[0] = value;
        }

        private static ResourceDictionary ControlColours {
            get => System.Windows.Application.Current.Resources.MergedDictionaries[1];
            set => System.Windows.Application.Current.Resources.MergedDictionaries[1] = value;
        }

        private static ResourceDictionary Controls {
            get => System.Windows.Application.Current.Resources.MergedDictionaries[2];
            set => System.Windows.Application.Current.Resources.MergedDictionaries[2] = value;
        }

        public static void SetTheme(ThemeType theme) {
            string themeName = theme.GetName();
            if (string.IsNullOrEmpty(themeName)) {
                return;
            }

            CurrentTheme = theme;
            ThemeDictionary = new ResourceDictionary() { Source = new Uri($"Themes/ColourDictionaries/{themeName}.xaml", UriKind.Relative) };
            ControlColours = new ResourceDictionary() { Source = new Uri("Themes/ControlColours.xaml", UriKind.Relative) };
            Controls = new ResourceDictionary() { Source = new Uri("Themes/Controls.xaml", UriKind.Relative) };
        }

        public static object GetResource(object key) {
            return ThemeDictionary[key];
        }

        public static SolidColorBrush GetBrush(string name) {
            return GetResource(name) is SolidColorBrush brush ? brush : new SolidColorBrush(Colors.White);
        }
    }
}