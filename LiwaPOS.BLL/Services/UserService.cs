using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Users.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsNoTrackingAsync(Expression<Func<User, bool>> filter = null)
        {
            var users = await _unitOfWork.Users.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(Expression<Func<User, bool>> filter = null)
        {
            var users = await _unitOfWork.Users.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserAsNoTrackingAsync(Expression<Func<User, bool>> filter = null)
        {
            var user = await _unitOfWork.Users.GetAsNoTrackingAsync(filter);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserAsync(Expression<Func<User, bool>> filter = null)
        {
            var user = await _unitOfWork.Users.GetAsync(filter);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByIdAsNoTrackingAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var user = _mapper.Map<User>(userDto);
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
