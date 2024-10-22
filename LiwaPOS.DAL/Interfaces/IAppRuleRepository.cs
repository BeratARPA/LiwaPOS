using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IAppRuleRepository
    {
        Task<IEnumerable<AppRule>> GetAllAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<IEnumerable<AppRule>> GetAllAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<AppRule> GetAsync(Expression<Func<AppRule, bool>> filter = null);       
        Task<AppRule> GetAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null);       
        Task<AppRule> GetByIdAsync(int id);
        Task<AppRule> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(AppRule entity);
        Task UpdateAsync(AppRule entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<AppRule, bool>> filter = null, IEnumerable<AppRule> entities = null);
    }
}
