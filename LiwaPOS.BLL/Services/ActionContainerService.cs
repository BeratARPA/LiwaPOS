using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class ActionContainerService : IActionContainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActionContainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddActionContainerAsync(ActionContainerDTO actionContainerDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var actionContainer = _mapper.Map<ActionContainer>(actionContainerDto);
                await _unitOfWork.ActionContainers.AddAsync(actionContainer);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllActionContainersAsync(Expression<Func<ActionContainer, bool>> filter = null, IEnumerable<ActionContainer> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.ActionContainers.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteActionContainerAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.ActionContainers.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<ActionContainerDTO>> GetAllActionContainersAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null)
        {
            var actionContainers = await _unitOfWork.ActionContainers.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<ActionContainerDTO>>(actionContainers);
        }

        public async Task<IEnumerable<ActionContainerDTO>> GetAllActionContainersAsync(Expression<Func<ActionContainer, bool>> filter = null)
        {
            var actionContainers = await _unitOfWork.ActionContainers.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<ActionContainerDTO>>(actionContainers);
        }

        public async Task<ActionContainerDTO> GetActionContainerAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null)
        {
            var actionContainer = await _unitOfWork.ActionContainers.GetAsNoTrackingAsync(filter);
            return _mapper.Map<ActionContainerDTO>(actionContainer);
        }

        public async Task<ActionContainerDTO> GetActionContainerAsync(Expression<Func<ActionContainer, bool>> filter = null)
        {
            var actionContainer = await _unitOfWork.ActionContainers.GetAsync(filter);
            return _mapper.Map<ActionContainerDTO>(actionContainer);
        }

        public async Task<ActionContainerDTO> GetActionContainerByIdAsNoTrackingAsync(int id)
        {
            var actionContainer = await _unitOfWork.ActionContainers.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<ActionContainerDTO>(actionContainer);
        }

        public async Task<ActionContainerDTO> GetActionContainerByIdAsync(int id)
        {
            var actionContainer = await _unitOfWork.ActionContainers.GetByIdAsync(id);
            return _mapper.Map<ActionContainerDTO>(actionContainer);
        }

        public async Task UpdateActionContainerAsync(ActionContainerDTO ActionContainerDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var actionContainer = _mapper.Map<ActionContainer>(ActionContainerDto);
                await _unitOfWork.ActionContainers.UpdateAsync(actionContainer);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
