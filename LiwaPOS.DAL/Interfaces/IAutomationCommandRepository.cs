using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IAutomationCommandRepository
    {
        Task<IEnumerable<AutomationCommand>> GetAllAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<IEnumerable<AutomationCommand>> GetAllAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommand> GetAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommand> GetAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommand> GetByIdAsync(int id);
        Task<AutomationCommand> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(AutomationCommand entity);
        Task UpdateAsync(AutomationCommand entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<AutomationCommand, bool>> filter = null, IEnumerable<AutomationCommand> entities = null);
    }
}
