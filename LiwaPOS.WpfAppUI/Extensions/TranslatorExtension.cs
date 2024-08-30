using LiwaPOS.BLL.Services;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Markup;

namespace LiwaPOS.WpfAppUI.Extensions
{
    public class TranslatorExtension : MarkupExtension
    {
        public string Key { get; set; }
        public string Suffix { get; set; } // Yeni eklenen Suffix özelliği

        public TranslatorExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                return string.Empty;
            }

            var localizationService = _staticServiceProvider?.GetService<LocalizationService>();

            if (localizationService == null)
            {
                LoggingService.LogErrorAsync("LocalizationService is not registered in the service provider.", typeof(TranslatorExtension).Name, Key, new InvalidOperationException());
                return Key; // Eğer servis yoksa yine anahtarı göster
            }

            // Default dil kodunu al
            string defaultLanguageJson = localizationService.LoadDefaultLanguageaAsync().GetAwaiter().GetResult();

            // JSON formatını çöz ve sadece 'Language' değerini al
            var defaultLanguageObj = JsonHelper.Deserialize<Dictionary<string, string>>(defaultLanguageJson);
            string defaultLanguage = defaultLanguageObj != null && defaultLanguageObj.TryGetValue("Language", out var lang) ? lang : "en";

            // İlgili dil dosyasını yükle
            string translationContent = localizationService.LoadLanguageFileAsync(defaultLanguage).GetAwaiter().GetResult();

            // JSON formatında ise deserialize edebilirsin
            var translations = JsonHelper.Deserialize<List<LanguageDTO>>(translationContent);

            // Eğer çevrilecek anahtar varsa onu döndür, yoksa anahtarı döndür
            var translation = translations?.FirstOrDefault(t => t.Key == Key);
            string translatedText = translation != null ? translation.Value : Key;

            // Suffix ekle
            string result = $"{translatedText}{Suffix}";

            // Eğer tasarım modundaysak, gerçek değeri döndür
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget
                && provideValueTarget.TargetObject.GetType().FullName == "System.Windows.SharedDp")
            {
                return result; // Tasarım modunda çeviriyi göster
            }

            return result;
        }


        private static IServiceProvider? _staticServiceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _staticServiceProvider = serviceProvider;
        }

        public static string Translate(string key, string suffix = "")
        {
            if (_staticServiceProvider == null)
            {
                LoggingService.LogErrorAsync("TranslatorExtension is not initialized. Call Initialize() with a valid IServiceProvider.", typeof(TranslatorExtension).Name, key, new InvalidOperationException());
                return string.Empty;
            }

            var localizationService = _staticServiceProvider.GetService<LocalizationService>();

            if (localizationService == null)
            {
                LoggingService.LogErrorAsync("LocalizationService is not registered in the service provider.", typeof(TranslatorExtension).Name, key, new InvalidOperationException());
                return string.Empty;
            }

            // Default dil kodunu al
            string defaultLanguageJson = localizationService.LoadDefaultLanguageaAsync().GetAwaiter().GetResult();

            // JSON formatını çöz ve sadece 'Language' değerini al
            var defaultLanguageObj = JsonHelper.Deserialize<Dictionary<string, string>>(defaultLanguageJson);
            string defaultLanguage = defaultLanguageObj != null && defaultLanguageObj.TryGetValue("Language", out var lang) ? lang : "en";

            // İlgili dil dosyasını yükle
            string translationContent = localizationService.LoadLanguageFileAsync(defaultLanguage).GetAwaiter().GetResult();

            var translations = JsonHelper.Deserialize<List<LanguageDTO>>(translationContent);

            var translation = translations?.FirstOrDefault(t => t.Key == key);
            string translatedText = translation != null ? translation.Value : key;

            return $"{translatedText}{suffix}";
        }
    }

}
