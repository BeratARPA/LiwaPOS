using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAutomationCommandService
    {
        Task<IEnumerable<AutomationCommandDTO>> GetAllAutomationCommandsAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<IEnumerable<AutomationCommandDTO>> GetAllAutomationCommandsAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommandDTO> GetAutomationCommandAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommandDTO> GetAutomationCommandAsNoTrackingAsync(Expression<Func<AutomationCommand, bool>> filter = null);
        Task<AutomationCommandDTO> GetAutomationCommandByIdAsync(int id);
        Task<AutomationCommandDTO> GetAutomationCommandByIdAsNoTrackingAsync(int id);
        Task AddAutomationCommandAsync(AutomationCommandDTO automationCommandDto);
        Task UpdateAutomationCommandAsync(AutomationCommandDTO automationCommandDto);
        Task DeleteAutomationCommandAsync(int id);
        Task DeleteAllAutomationCommandsAsync(Expression<Func<AutomationCommand, bool>> filter = null, IEnumerable<AutomationCommand> entities = null);
    }
}
