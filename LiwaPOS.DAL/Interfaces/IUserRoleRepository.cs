using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<IEnumerable<UserRole>> GetAllAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRole> GetAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRole> GetAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRole> GetByIdAsync(int id);
        Task<UserRole> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(UserRole entity);
        Task UpdateAsync(UserRole entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<UserRole, bool>> filter = null, IEnumerable<UserRole> entities = null);
    }
}
