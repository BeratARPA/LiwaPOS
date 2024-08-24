using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.Shared.Extensions
{
    public class TranslatorExtension
    {
        public async static Task<string> Translate(string key)
        {
            // Default dil kodunu al
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.ConfigurationsPath);
            string defaultLanguageJson = FileExtension.Exists(Path.Combine(FolderLocationsHelper.ConfigurationsPath, "DefaultLanguage.json")) ? FileExtension.ReadText(Path.Combine(FolderLocationsHelper.ConfigurationsPath, "DefaultLanguage.json")) : "en-us";

            // JSON formatını çöz ve sadece 'Language' değerini al
            var defaultLanguageObj = JsonHelper.Deserialize<Dictionary<string, string>>(defaultLanguageJson);
            string defaultLanguage = defaultLanguageObj != null && defaultLanguageObj.TryGetValue("Language", out var lang) ? lang : "en";

            // İlgili dil dosyasını yükle          
            string filePath = Path.Combine(FolderLocationsHelper.LanguagesPath, $"{defaultLanguage}.json");
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.LanguagesPath);
            string translationContent = FileExtension.Exists(filePath) ? FileExtension.ReadText(filePath) : "";

            var translations = JsonHelper.Deserialize<List<LanguageDTO>>(translationContent);

            var translation = translations?.FirstOrDefault(t => t.Key == key);
            return translation != null ? translation.Value : key;
        }
    }
}
