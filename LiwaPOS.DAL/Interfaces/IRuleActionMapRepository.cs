using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IRuleActionMapRepository
    {
        Task<IEnumerable<RuleActionMap>> GetAllAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMap> GetAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMap> GetByIdAsync(int id);
        Task AddAsync(RuleActionMap entity);
        Task UpdateAsync(RuleActionMap entity);
        Task DeleteAsync(int id);
    }
}
