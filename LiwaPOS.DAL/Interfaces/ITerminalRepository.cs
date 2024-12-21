using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface ITerminalRepository
    {
        Task<IEnumerable<Terminal>> GetAllAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<IEnumerable<Terminal>> GetAllAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<Terminal> GetAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<Terminal> GetAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<Terminal> GetByIdAsync(int id);
        Task<Terminal> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(Terminal entity);
        Task UpdateAsync(Terminal entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<Terminal, bool>> filter = null, IEnumerable<Terminal> entities = null);
    }
}
