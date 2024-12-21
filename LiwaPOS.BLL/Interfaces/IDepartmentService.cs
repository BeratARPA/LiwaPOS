using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(Expression<Func<Department, bool>> filter = null);
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null);
        Task<DepartmentDTO> GetDepartmentAsync(Expression<Func<Department, bool>> filter = null);
        Task<DepartmentDTO> GetDepartmentAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null);
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task<DepartmentDTO> GetDepartmentByIdAsNoTrackingAsync(int id);
        Task AddDepartmentAsync(DepartmentDTO DepartmentDto);
        Task UpdateDepartmentAsync(DepartmentDTO DepartmentDto);
        Task DeleteDepartmentAsync(int id);
        Task DeleteAllDepartmentsAsync(Expression<Func<Department, bool>> filter = null, IEnumerable<Department> entities = null);
    }
}
