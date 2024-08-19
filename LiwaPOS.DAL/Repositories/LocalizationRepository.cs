using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;

namespace LiwaPOS.DAL.Repositories
{
    public class LocalizationRepository
    {
        public async Task SaveLanguageFileAsync(string languageCode, string content)
        {
            string filePath = Path.Combine(FolderLocationsHelper.LanguagesPath, $"{languageCode}.json");
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.LanguagesPath);
            await FileExtension.WriteTextAsync(filePath, content);
        }

        public async Task<string> LoadLanguageFileAsync(string languageCode)
        {
            string filePath = Path.Combine(FolderLocationsHelper.LanguagesPath, $"{languageCode}.json");
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.LanguagesPath);
            return FileExtension.Exists(filePath) ? FileExtension.ReadText(filePath) : "";
        }

        public async Task SaveDefaultLanguageAsync(string languageCode)
        {
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.ConfigurationsPath);
            await FileExtension.WriteTextAsync(Path.Combine(FolderLocationsHelper.ConfigurationsPath, "DefaultLanguage.json"), languageCode);
        }

        public async Task<string> LoadDefaultLanguage()
        {
            await DirectoryExtension.CreateIfNotExistsAsync(FolderLocationsHelper.ConfigurationsPath);
            return FileExtension.Exists(Path.Combine(FolderLocationsHelper.ConfigurationsPath, "DefaultLanguage.json")) ? FileExtension.ReadText(Path.Combine(FolderLocationsHelper.ConfigurationsPath, "DefaultLanguage.json")) : "en-us";
        }
    }
}
