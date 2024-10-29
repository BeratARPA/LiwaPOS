using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class AutomationCommandService : IAutomationCommandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AutomationCommandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAutomationCommandAsync(AutomationCommandDTO automationCommandDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var automationCommand = _mapper.Map<AutomationCommand>(automationCommandDto);
                await _unitOfWork.AutomationCommands.AddAsync(automationCommand);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllAutomationCommandsAsync(Expression<Func<AutomationCommand, bool>> filter = null, IEnumerable<AutomationCommand> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AutomationCommands.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }      

        public async Task DeleteAutomationCommandAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AutomationCommands.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<AutomationCommandDTO>> GetAllAutomationCommandsAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null)
        {
            var automationCommands = await _unitOfWork.AutomationCommands.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<AutomationCommandDTO>>(automationCommands);
        }

        public async Task<IEnumerable<AutomationCommandDTO>> GetAllAutomationCommandsAsync(Expression<Func<AutomationCommand, bool>> filter = null)
        {
            var automationCommands = await _unitOfWork.AutomationCommands.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<AutomationCommandDTO>>(automationCommands);
        }

        public async Task<AutomationCommandDTO> GetAutomationCommandAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null)
        {
            var automationCommand = await _unitOfWork.AutomationCommands.GetAsNoTrackingAsync(filter);
            return _mapper.Map<AutomationCommandDTO>(automationCommand);
        }

        public async Task<AutomationCommandDTO> GetAutomationCommandAsync(Expression<Func<AutomationCommand, bool>> filter = null)
        {
            var automationCommand = await _unitOfWork.AutomationCommands.GetAsync(filter);
            return _mapper.Map<AutomationCommandDTO>(automationCommand);
        }

        public async Task<AutomationCommandDTO> GetAutomationCommandByIdAsNoTrackingAsync(int id)
        {
            var automationCommand = await _unitOfWork.AutomationCommands.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<AutomationCommandDTO>(automationCommand);
        }

        public async Task<AutomationCommandDTO> GetAutomationCommandByIdAsync(int id)
        {
            var automationCommand = await _unitOfWork.AutomationCommands.GetByIdAsync(id);
            return _mapper.Map<AutomationCommandDTO>(automationCommand);
        }

        public async Task UpdateAutomationCommandAsync(AutomationCommandDTO automationCommandDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var automationCommand = _mapper.Map<AutomationCommand>(automationCommandDto);
                await _unitOfWork.AutomationCommands.UpdateAsync(automationCommand);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
