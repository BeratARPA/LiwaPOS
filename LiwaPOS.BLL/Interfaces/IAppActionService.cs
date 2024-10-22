using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAppActionService
    {
        Task<IEnumerable<AppActionDTO>> GetAllAppActionsAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<IEnumerable<AppActionDTO>> GetAllAppActionsAsNoTrackingAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppActionDTO> GetAppActionAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppActionDTO> GetAppActionAsNoTrackingAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppActionDTO> GetAppActionByIdAsync(int id);
        Task<AppActionDTO> GetAppActionByIdAsNoTrackingAsync(int id);
        Task AddAppActionAsync(AppActionDTO appActionDto);
        Task UpdateAppActionAsync(AppActionDTO appActionDto);
        Task DeleteAppActionAsync(int id);
        Task DeleteAllAppActionsAsync(Expression<Func<AppAction, bool>> filter = null, IEnumerable<AppAction> entities = null);
    }
}
