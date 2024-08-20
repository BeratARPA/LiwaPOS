using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IRuleActionMapService
    {
        Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMapDTO> GetRuleActionMapAsync(Expression<Func<RuleActionMap, bool>> filter = null);
        Task<RuleActionMapDTO> GetRuleActionMapByIdAsync(int id);
        Task AddRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto);
        Task UpdateRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto);
        Task DeleteRuleActionMapAsync(int id);
    }
}
