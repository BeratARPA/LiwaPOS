using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IActionContainerRepository
    {
        Task<IEnumerable<ActionContainer>> GetAllAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<IEnumerable<ActionContainer>> GetAllAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainer> GetAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainer> GetAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainer> GetByIdAsync(int id);
        Task<ActionContainer> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(ActionContainer entity);
        Task UpdateAsync(ActionContainer entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<ActionContainer, bool>> filter = null, IEnumerable<ActionContainer> entities = null);
    }
}
