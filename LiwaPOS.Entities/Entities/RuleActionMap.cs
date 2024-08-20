namespace LiwaPOS.Entities.Entities
{
    public class RuleActionMap : BaseEntity
    {
        public int AppRuleId { get; set; }
        public int AppActionId { get; set; }
        public int SortOrder { get; set; }

        public AppRule? AppRule { get; set; }
        public AppAction? AppAction { get; set; }
    }
}
