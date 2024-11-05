using LiwaPOS.Shared.Enums;

namespace LiwaPOS.WpfAppUI.Extensions
{
    public static class LanguageTypeExtension
    {
        public static string ToShortString(this LanguageType languageType)
        {
            return languageType switch
            {
                LanguageType.Türkçe => "tr",
                LanguageType.English => "en",
                _ => string.Empty
            };
        }

        public static LanguageType ToShortString(string languageCode)
        {
            return languageCode switch
            {
                "tr" => LanguageType.Türkçe,
                "en" => LanguageType.English,
                _ => LanguageType.Türkçe
            };
        }
    }
}
