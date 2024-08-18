using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAppActionService
    {
        Task<IEnumerable<AppActionDTO>> GetAllAppActionsAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppActionDTO> GetAppActionAsync(Expression<Func<AppAction, bool>> filter = null);
        Task<AppActionDTO> GetAppActionByIdAsync(int id);
        Task AddAppActionAsync(AppActionDTO appActionDto);
        Task UpdateAppActionAsync(AppActionDTO appActionDto);
        Task DeleteAppActionAsync(int id);
    }
}
