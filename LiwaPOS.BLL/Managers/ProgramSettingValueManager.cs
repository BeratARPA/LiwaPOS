using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Managers
{
    public class ProgramSettingValueManager
    {
        private readonly IUserService _userService;
        private readonly ITerminalService _terminalService;
        private readonly IProgramSettingValueService _programSettingValueService;

        public ProgramSettingValueManager(
            IUserService userService,
            ITerminalService terminalService,
            IProgramSettingValueService programSettingValueService)
        {
            _userService = userService;
            _terminalService = terminalService;
            _programSettingValueService = programSettingValueService;
        }

        public async Task<UserDTO> GetUserByUserName(string name)
        {
            return await _userService.GetUserAsync(x => x.Name == name);
        }

        public async Task<TerminalDTO> GetTerminalByName(string name)
        {
            return await _terminalService.GetTerminalAsync(x => x.Name == name);
        }

        public async Task<TerminalDTO> GetDefaultTerminal()
        {
            return await _terminalService.GetTerminalAsync(x => x.IsDefault);
        }

        // Ayarları isme göre kaydetme/güncelleme
        public async Task SetLocalSettingAsync(string name, string value)
        {
            var existingSetting = await _programSettingValueService.GetProgramSettingValueAsNoTrackingAsync(e => e.Name == name);
            if (existingSetting != null)
            {
                // Güncelleme
                existingSetting.Value = value;
                await _programSettingValueService.UpdateProgramSettingValueAsync(existingSetting);
            }
            else
            {
                // Yeni ekleme
                var newSetting = new ProgramSettingValueDTO
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = name,
                    Value = value
                };
                await _programSettingValueService.AddProgramSettingValueAsync(newSetting);
            }
        }

        // İsime göre ayar okuma
        public async Task<string> GetLocalSettingAsync(string name)
        {
            var setting = await _programSettingValueService.GetProgramSettingValueAsync(e => e.Name == name);
            return setting?.Value ?? "";
        }

        // Tüm ayarları dictionary olarak alma
        public async Task<Dictionary<string, string>> GetAllLocalSettingsAsync()
        {
            var settings = await _programSettingValueService.GetAllProgramSettingValuesAsync();
            return settings.ToDictionary(s => s.Name, s => s.Value);
        }

        // Ayar silme
        public async Task DeleteLocalSettingAsync(string name)
        {
            var existingSetting = await _programSettingValueService.GetProgramSettingValueAsync(e => e.Name == name);
            if (existingSetting != null)
            {
                await _programSettingValueService.DeleteProgramSettingValueAsync(existingSetting.Id);
            }
        }

        // İzleme olmadan hızlı okuma
        public async Task<string> GetLocalSettingFastAsync(string name)
        {
            var setting = await _programSettingValueService.GetProgramSettingValueAsNoTrackingAsync(e => e.Name == name);
            return setting?.Value ?? "";
        }
    }
}
