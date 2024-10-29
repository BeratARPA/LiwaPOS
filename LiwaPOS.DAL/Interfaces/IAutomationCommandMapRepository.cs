using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IAutomationCommandMapRepository
    {
        Task<IEnumerable<AutomationCommandMap>> GetAllAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<IEnumerable<AutomationCommandMap>> GetAllAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMap> GetAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMap> GetAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMap> GetByIdAsync(int id);
        Task<AutomationCommandMap> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(AutomationCommandMap entity);
        Task UpdateAsync(AutomationCommandMap entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<AutomationCommandMap, bool>> filter = null, IEnumerable<AutomationCommandMap> entities = null);
    }
}
