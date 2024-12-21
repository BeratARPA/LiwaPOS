using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface ITerminalService
    {
        Task<IEnumerable<TerminalDTO>> GetAllTerminalsAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<IEnumerable<TerminalDTO>> GetAllTerminalsAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<TerminalDTO> GetTerminalAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<TerminalDTO> GetTerminalAsNoTrackingAsync(Expression<Func<Terminal, bool>> filter = null);
        Task<TerminalDTO> GetTerminalByIdAsync(int id);
        Task<TerminalDTO> GetTerminalByIdAsNoTrackingAsync(int id);
        Task AddTerminalAsync(TerminalDTO TerminalDto);
        Task UpdateTerminalAsync(TerminalDTO TerminalDto);
        Task DeleteTerminalAsync(int id);
        Task DeleteAllTerminalsAsync(Expression<Func<Terminal, bool>> filter = null, IEnumerable<Terminal> entities = null);
    }
}
