using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IRuleActionMapService
    {
        Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMapDTO> GetRuleActionMapAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMapDTO> GetRuleActionMapAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMapDTO> GetRuleActionMapByIdAsync(int id);
        Task<RuleActionMapDTO> GetRuleActionMapByIdAsNoTrackingAsync(int id);
        Task AddRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto);
        Task UpdateRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto);
        Task DeleteRuleActionMapAsync(int id);
        Task DeleteAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null, IEnumerable<RuleActionMap> entities = null);
    }
}
