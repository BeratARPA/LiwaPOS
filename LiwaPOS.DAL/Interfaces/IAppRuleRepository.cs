using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IAppRuleRepository
    {
        Task<IEnumerable<AppRule>> GetAllAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<AppRule> GetAsync(Expression<Func<AppRule, bool>> filter = null);       
        Task<AppRule> GetByIdAsync(int id);
        Task AddAsync(AppRule entity);
        Task UpdateAsync(AppRule entity);
        Task DeleteAsync(int id);
    }
}
