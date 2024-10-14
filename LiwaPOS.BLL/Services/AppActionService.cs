using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class AppActionService : IAppActionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppActionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppActionDTO>> GetAllAppActionsAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            var appActions = await _unitOfWork.AppActions.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<AppActionDTO>>(appActions);
        }

        public async Task<AppActionDTO> GetAppActionAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            var appAction = await _unitOfWork.AppActions.GetAsync(filter);
            return _mapper.Map<AppActionDTO>(appAction);
        }

        public async Task<AppActionDTO> GetAppActionByIdAsync(int id)
        {
            var appAction = await _unitOfWork.AppActions.GetByIdAsync(id);
            return _mapper.Map<AppActionDTO>(appAction);
        }

        public async Task AddAppActionAsync(AppActionDTO appActionDto)
        {
            var appAction = _mapper.Map<AppAction>(appActionDto);
            await _unitOfWork.AppActions.AddAsync(appAction);
            _unitOfWork.Commit();
        }

        public async Task UpdateAppActionAsync(AppActionDTO appActionDto)
        {
            var appAction = _mapper.Map<AppAction>(appActionDto);
            await _unitOfWork.AppActions.UpdateAsync(appAction);
            _unitOfWork.Commit();
        }

        public async Task DeleteAppActionAsync(int id)
        {
            await _unitOfWork.AppActions.DeleteAsync(id);
            _unitOfWork.Commit();
        }
    }
}
