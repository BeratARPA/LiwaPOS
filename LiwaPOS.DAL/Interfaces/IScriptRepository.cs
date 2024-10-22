using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IScriptRepository
    {
        Task<IEnumerable<Script>> GetAllAsync(Expression<Func<Script, bool>> filter = null);
        Task<IEnumerable<Script>> GetAllAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null);
        Task<Script> GetAsync(Expression<Func<Script, bool>> filter = null);
        Task<Script> GetAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null);
        Task<Script> GetByIdAsync(int id);
        Task<Script> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(Script entity);
        Task UpdateAsync(Script entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<Script, bool>> filter = null, IEnumerable<Script> entities = null);
    }
}
