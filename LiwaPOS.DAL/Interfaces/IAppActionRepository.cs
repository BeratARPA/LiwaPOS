using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IAppActionRepository
    {
        Task<IEnumerable<AppAction>> GetAllAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppAction> GetAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppAction> GetByIdAsync(int id);
        Task AddAsync(AppAction entity);
        Task UpdateAsync(AppAction entity);
        Task DeleteAsync(int id);
    }
}
