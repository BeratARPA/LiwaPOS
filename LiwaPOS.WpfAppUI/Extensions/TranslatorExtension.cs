using LiwaPOS.BLL.Services;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.WpfAppUI.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace LiwaPOS.WpfAppUI.Extensions
{
    public class TranslatorExtension : MarkupExtension
    {
        public string Key { get; set; }

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

            var localizationService = _staticServiceProvider.GetService<LocalizationService>();

            if (localizationService == null)
            {
                throw new InvalidOperationException("LocalizationService is not registered in the service provider.");
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
            return translation != null ? translation.Value : Key;
        }

        private static IServiceProvider? _staticServiceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _staticServiceProvider = serviceProvider;
        }

        public static string Translate(string key)
        {
            if (_staticServiceProvider == null)
            {
                throw new InvalidOperationException("TranslatorExtension is not initialized. Call Initialize() with a valid IServiceProvider.");
            }

            var localizationService = _staticServiceProvider.GetService<LocalizationService>();

            if (localizationService == null)
            {
                throw new InvalidOperationException("LocalizationService is not registered in the service provider.");
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
            return translation != null ? translation.Value : key;
        }
    }
}
