using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync(Expression<Func<Permission, bool>> filter = null);
        Task<IEnumerable<Permission>> GetAllAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null);
        Task<Permission> GetAsync(Expression<Func<Permission, bool>> filter = null);
        Task<Permission> GetAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null);
        Task<Permission> GetByIdAsync(int id);
        Task<Permission> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(Permission entity);
        Task UpdateAsync(Permission entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<Permission, bool>> filter = null, IEnumerable<Permission> entities = null);
    }
}
