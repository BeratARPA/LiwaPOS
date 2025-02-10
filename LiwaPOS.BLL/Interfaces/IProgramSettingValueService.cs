using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IProgramSettingValueService
    {
        Task<IEnumerable<ProgramSettingValueDTO>> GetAllProgramSettingValuesAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<IEnumerable<ProgramSettingValueDTO>> GetAllProgramSettingValuesAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValueDTO> GetProgramSettingValueAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValueDTO> GetProgramSettingValueAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValueDTO> GetProgramSettingValueByIdAsync(int id);
        Task<ProgramSettingValueDTO> GetProgramSettingValueByIdAsNoTrackingAsync(int id);
        Task AddProgramSettingValueAsync(ProgramSettingValueDTO programSettingValueDto);
        Task UpdateProgramSettingValueAsync(ProgramSettingValueDTO programSettingValueDto);
        Task DeleteProgramSettingValueAsync(int id);
        Task DeleteAllProgramSettingValuesAsync(Expression<Func<ProgramSettingValue, bool>> filter = null, IEnumerable<ProgramSettingValue> entities = null);
    }
}
