using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TerminalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddTerminalAsync(TerminalDTO terminalDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var terminal = _mapper.Map<Terminal>(terminalDto);
                await _unitOfWork.Terminals.AddAsync(terminal);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllTerminalsAsync(Expression<Func<Terminal, bool>> filter = null, IEnumerable<Terminal> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Terminals.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteTerminalAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Terminals.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<TerminalDTO>> GetAllTerminalsAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null)
        {
            var terminals = await _unitOfWork.Terminals.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<TerminalDTO>>(terminals);
        }

        public async Task<IEnumerable<TerminalDTO>> GetAllTerminalsAsync(Expression<Func<Terminal, bool>> filter = null)
        {
            var terminals = await _unitOfWork.Terminals.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<TerminalDTO>>(terminals);
        }

        public async Task<TerminalDTO> GetTerminalAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null)
        {
            var terminal = await _unitOfWork.Terminals.GetAsNoTrackingAsync(filter);
            return _mapper.Map<TerminalDTO>(terminal);
        }

        public async Task<TerminalDTO> GetTerminalAsync(Expression<Func<Terminal, bool>> filter = null)
        {
            var terminal = await _unitOfWork.Terminals.GetAsync(filter);
            return _mapper.Map<TerminalDTO>(terminal);
        }

        public async Task<TerminalDTO> GetTerminalByIdAsNoTrackingAsync(int id)
        {
            var terminal = await _unitOfWork.Terminals.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<TerminalDTO>(terminal);
        }

        public async Task<TerminalDTO> GetTerminalByIdAsync(int id)
        {
            var terminal = await _unitOfWork.Terminals.GetByIdAsync(id);
            return _mapper.Map<TerminalDTO>(terminal);
        }

        public async Task UpdateTerminalAsync(TerminalDTO TerminalDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var terminal = _mapper.Map<Terminal>(TerminalDto);
                await _unitOfWork.Terminals.UpdateAsync(terminal);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
