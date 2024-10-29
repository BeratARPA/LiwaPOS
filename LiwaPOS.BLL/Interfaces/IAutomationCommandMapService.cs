using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAutomationCommandMapService
    {
        Task<IEnumerable<AutomationCommandMapDTO>> GetAllAutomationCommandMapsAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<IEnumerable<AutomationCommandMapDTO>> GetAllAutomationCommandMapsAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMapDTO> GetAutomationCommandMapAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMapDTO> GetAutomationCommandMapAsNoTrackingAsync(Expression<Func<AutomationCommandMap, bool>> filter = null);
        Task<AutomationCommandMapDTO> GetAutomationCommandMapByIdAsync(int id);
        Task<AutomationCommandMapDTO> GetAutomationCommandMapByIdAsNoTrackingAsync(int id);
        Task AddAutomationCommandMapAsync(AutomationCommandMapDTO automationCommandMapDto);
        Task UpdateAutomationCommandMapAsync(AutomationCommandMapDTO automationCommandMapDto);
        Task DeleteAutomationCommandMapAsync(int id);
        Task DeleteAllAutomationCommandMapsAsync(Expression<Func<AutomationCommandMap, bool>> filter = null, IEnumerable<AutomationCommandMap> entities = null);
    }
}
