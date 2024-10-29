using LiwaPOS.DAL.Repositories;

namespace LiwaPOS.BLL.Services
{
    public class LocalizationService
    {
        private readonly LocalizationRepository _localizationRepository;

        public LocalizationService(LocalizationRepository localizationRepository)
        {
            _localizationRepository = localizationRepository;
        }
     
        public async Task SaveLanguageFileAsync(string languageCode, string content)
        {
            await _localizationRepository.SaveLanguageFileAsync(languageCode, content);
        }

        public async Task<string> LoadLanguageFileAsync(string languageCode)
        {
            return await _localizationRepository.LoadLanguageFileAsync(languageCode);
        }
    }
}
