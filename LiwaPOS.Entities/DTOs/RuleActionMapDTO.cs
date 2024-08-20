using LiwaPOS.Entities.Entities;

namespace LiwaPOS.Entities.DTOs
{
    public class RuleActionMapDTO
    {
        public int Id { get; set; }
        public int AppRuleId { get; set; }
        public int AppActionId { get; set; }
        public int SortOrder { get; set; }

        public AppRule? AppRule { get; set; }
        public AppAction? AppAction { get; set; }
    }
}
