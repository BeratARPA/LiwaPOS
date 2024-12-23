using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserRoleAsync(UserRoleDTO userRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(userRoleDto);
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.UserRoles.AddAsync(userRole);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllUserRolesAsync(Expression<Func<UserRole, bool>> filter = null, IEnumerable<UserRole> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.UserRoles.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteUserRoleAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.UserRoles.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null)
        {
            var userRoles = await _unitOfWork.UserRoles.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<UserRoleDTO>>(userRoles);
        }

        public async Task<IEnumerable<UserRoleDTO>> GetAllUserRolesAsync(Expression<Func<UserRole, bool>> filter = null)
        {
            var userRoles = await _unitOfWork.UserRoles.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<UserRoleDTO>>(userRoles);
        }

        public async Task<UserRoleDTO> GetUserRoleAsNoTrackingAsync(Expression<Func<UserRole, bool>> filter = null)
        {
            var userRole = await _unitOfWork.UserRoles.GetAsNoTrackingAsync(filter);
            return _mapper.Map<UserRoleDTO>(userRole);
        }

        public async Task<UserRoleDTO> GetUserRoleAsync(Expression<Func<UserRole, bool>> filter = null)
        {
            var userRole = await _unitOfWork.UserRoles.GetAsync(filter);
            return _mapper.Map<UserRoleDTO>(userRole);
        }

        public async Task<UserRoleDTO> GetUserRoleByIdAsNoTrackingAsync(int id)
        {
            var userRole = await _unitOfWork.UserRoles.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<UserRoleDTO>(userRole);
        }

        public async Task<UserRoleDTO> GetUserRoleByIdAsync(int id)
        {
            var userRole = await _unitOfWork.UserRoles.GetByIdAsync(id);
            return _mapper.Map<UserRoleDTO>(userRole);
        }

        public async Task UpdateUserRoleAsync(UserRoleDTO userRoleDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var userRole = _mapper.Map<UserRole>(userRoleDto);
                await _unitOfWork.UserRoles.UpdateAsync(userRole);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
