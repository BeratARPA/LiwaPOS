using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class AutomationCommandMapService : IAutomationCommandMapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AutomationCommandMapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAutomationCommandMapAsync(AutomationCommandMapDTO automationCommandMapDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var automationCommandMap = _mapper.Map<AutomationCommandMap>(automationCommandMapDto);
                await _unitOfWork.AutomationCommandMaps.AddAsync(automationCommandMap);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllAutomationCommandMapsAsync(Expression<Func<AutomationCommandMap, bool>> filter = null, IEnumerable<AutomationCommandMap> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AutomationCommandMaps.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAutomationCommandMapAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AutomationCommandMaps.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<AutomationCommandMapDTO>> GetAllAutomationCommandMapsAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null)
        {
            var automationCommandMaps = await _unitOfWork.AutomationCommandMaps.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<AutomationCommandMapDTO>>(automationCommandMaps);
        }

        public async Task<IEnumerable<AutomationCommandMapDTO>> GetAllAutomationCommandMapsAsync(Expression<Func<AutomationCommandMap, bool>> filter = null)
        {
            var automationCommandMaps = await _unitOfWork.AutomationCommandMaps.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<AutomationCommandMapDTO>>(automationCommandMaps);
        }

        public async Task<AutomationCommandMapDTO> GetAutomationCommandMapAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null)
        {
            var automationCommandMap = await _unitOfWork.AutomationCommandMaps.GetAsNoTrackingAsync(filter);
            return _mapper.Map<AutomationCommandMapDTO>(automationCommandMap);
        }

        public async Task<AutomationCommandMapDTO> GetAutomationCommandMapAsync(Expression<Func<AutomationCommandMap, bool>> filter = null)
        {
            var automationCommandMap = await _unitOfWork.AutomationCommandMaps.GetAsync(filter);
            return _mapper.Map<AutomationCommandMapDTO>(automationCommandMap);
        }

        public async Task<AutomationCommandMapDTO> GetAutomationCommandMapByIdAsNoTrackingAsync(int id)
        {
            var automationCommandMap = await _unitOfWork.AutomationCommandMaps.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<AutomationCommandMapDTO>(automationCommandMap);
        }

        public async Task<AutomationCommandMapDTO> GetAutomationCommandMapByIdAsync(int id)
        {
            var automationCommandMap = await _unitOfWork.AutomationCommandMaps.GetByIdAsync(id);
            return _mapper.Map<AutomationCommandMapDTO>(automationCommandMap);
        }

        public async Task UpdateAutomationCommandMapAsync(AutomationCommandMapDTO automationCommandMapDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var automationCommandMap = _mapper.Map<AutomationCommandMap>(automationCommandMapDto);
                await _unitOfWork.AutomationCommandMaps.UpdateAsync(automationCommandMap);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
