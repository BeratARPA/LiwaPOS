using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAppRuleService
    {
        Task<IEnumerable<AppRuleDTO>> GetAllAppRulesAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<IEnumerable<AppRuleDTO>> GetAllAppRulesAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<AppRuleDTO> GetAppRuleAsync(Expression<Func<AppRule, bool>> filter = null);     
        Task<AppRuleDTO> GetAppRuleAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null);     
        Task<AppRuleDTO> GetAppRuleByIdAsync(int id);
        Task<AppRuleDTO> GetAppRuleByIdAsNoTrackingAsync(int id);
        Task AddAppRuleAsync(AppRuleDTO appRuleDto);
        Task UpdateAppRuleAsync(AppRuleDTO appRuleDto);
        Task DeleteAppRuleAsync(int id);
        Task DeleteAllAppRulesAsync(Expression<Func<AppRule, bool>> filter = null, IEnumerable<AppRule> entities = null);
    }
}
