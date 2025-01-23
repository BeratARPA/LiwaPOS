using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IActionContainerService
    {
        Task<IEnumerable<ActionContainerDTO>> GetAllActionContainersAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<IEnumerable<ActionContainerDTO>> GetAllActionContainersAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainerDTO> GetActionContainerAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainerDTO> GetActionContainerAsNoTrackingAsync(Expression<Func<ActionContainer, bool>> filter = null);
        Task<ActionContainerDTO> GetActionContainerByIdAsync(int id);
        Task<ActionContainerDTO> GetActionContainerByIdAsNoTrackingAsync(int id);
        Task AddActionContainerAsync(ActionContainerDTO ActionContainerDto);
        Task UpdateActionContainerAsync(ActionContainerDTO ActionContainerDto);
        Task DeleteActionContainerAsync(int id);
        Task DeleteAllActionContainersAsync(Expression<Func<ActionContainer, bool>> filter = null, IEnumerable<ActionContainer> entities = null);
    }
}
