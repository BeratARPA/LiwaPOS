using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRoleDTO> GetUserRoleAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRoleDTO> GetUserRoleAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null);
        Task<UserRoleDTO> GetUserRoleByIdAsync(int id);
        Task<UserRoleDTO> GetUserRoleByIdAsNoTrackingAsync(int id);
        Task AddUserRoleAsync(UserRoleDTO UserRoleDto);
        Task UpdateUserRoleAsync(UserRoleDTO UserRoleDto);
        Task DeleteUserRoleAsync(int id);
        Task DeleteAllUserRolesAsync(Expression<Func<UserRole, bool>> filter = null, IEnumerable<UserRole> entities = null);
    }
}
