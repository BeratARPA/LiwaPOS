using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddPermissionAsync(PermissionDTO permissionDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var permission = _mapper.Map<Permission>(permissionDto);
                await _unitOfWork.Permissions.AddAsync(permission);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllPermissionsAsync(Expression<Func<Permission, bool>> filter = null, IEnumerable<Permission> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Permissions.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Permissions.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null)
        {
            var permissions = await _unitOfWork.Permissions.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync(Expression<Func<Permission, bool>> filter = null)
        {
            var permissions = await _unitOfWork.Permissions.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
        }

        public async Task<PermissionDTO> GetPermissionAsNoTrackingAsync(Expression<Func<Permission, bool>> filter = null)
        {
            var permission = await _unitOfWork.Permissions.GetAsNoTrackingAsync(filter);
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<PermissionDTO> GetPermissionAsync(Expression<Func<Permission, bool>> filter = null)
        {
            var permission = await _unitOfWork.Permissions.GetAsync(filter);
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<PermissionDTO> GetPermissionByIdAsNoTrackingAsync(int id)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<PermissionDTO> GetPermissionByIdAsync(int id)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(id);
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task UpdatePermissionAsync(PermissionDTO permissionDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var permission = _mapper.Map<Permission>(permissionDto);
                await _unitOfWork.Permissions.UpdateAsync(permission);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
