using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(Expression<Func<User, bool>> filter = null);
        Task<IEnumerable<UserDTO>> GetAllUsersAsNoTrackingAsync(Expression<Func<User, bool>> filter = null);
        Task<UserDTO> GetUserAsync(Expression<Func<User, bool>> filter = null);
        Task<UserDTO> GetUserAsNoTrackingAsync(Expression<Func<User, bool>> filter = null);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByIdAsNoTrackingAsync(int id);
        Task AddUserAsync(UserDTO appRuleDto);
        Task UpdateUserAsync(UserDTO appRuleDto);
        Task DeleteUserAsync(int id);
    }
}
