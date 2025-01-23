using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync(Expression<Func<Permission, bool>> filter = null);
        Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null);
        Task<PermissionDTO> GetPermissionAsync(Expression<Func<Permission, bool>> filter = null);
        Task<PermissionDTO> GetPermissionAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null);
        Task<PermissionDTO> GetPermissionByIdAsync(int id);
        Task<PermissionDTO> GetPermissionByIdAsNoTrackingAsync(int id);
        Task AddPermissionAsync(PermissionDTO PermissionDto);
        Task UpdatePermissionAsync(PermissionDTO PermissionDto);
        Task DeletePermissionAsync(int id);
        Task DeleteAllPermissionsAsync(Expression<Func<Permission, bool>> filter = null, IEnumerable<Permission> entities = null);
    }
}
