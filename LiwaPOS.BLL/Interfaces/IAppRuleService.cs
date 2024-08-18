using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IAppRuleService
    {
        Task<IEnumerable<AppRuleDTO>> GetAllAppRulesAsync(Expression<Func<AppRule, bool>> filter = null);
        Task<AppRuleDTO> GetAppRuleAsync(Expression<Func<AppRule, bool>> filter = null);     
        Task<AppRuleDTO> GetAppRuleByIdAsync(int id);
        Task AddAppRuleAsync(AppRuleDTO appRuleDto);
        Task UpdateAppRuleAsync(AppRuleDTO appRuleDto);
        Task DeleteAppRuleAsync(int id);
    }
}
