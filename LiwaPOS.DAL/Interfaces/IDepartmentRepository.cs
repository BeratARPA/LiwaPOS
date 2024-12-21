using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null);
        Task<IEnumerable<Department>> GetAllAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null);
        Task<Department> GetAsync(Expression<Func<Department, bool>> filter = null);
        Task<Department> GetAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null);
        Task<Department> GetByIdAsync(int id);
        Task<Department> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(Department entity);
        Task UpdateAsync(Department entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<Department, bool>> filter = null, IEnumerable<Department> entities = null);
    }
}
