using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IProgramSettingValueRepository
    {
        Task<IEnumerable<ProgramSettingValue>> GetAllAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<IEnumerable<ProgramSettingValue>> GetAllAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValue> GetAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValue> GetAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null);
        Task<ProgramSettingValue> GetByIdAsync(int id);
        Task<ProgramSettingValue> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(ProgramSettingValue entity);
        Task UpdateAsync(ProgramSettingValue entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<ProgramSettingValue, bool>> filter = null, IEnumerable<ProgramSettingValue> entities = null);
    }
}
