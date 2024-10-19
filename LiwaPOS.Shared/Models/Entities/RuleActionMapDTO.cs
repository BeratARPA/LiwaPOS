namespace LiwaPOS.Shared.Models.Entities
{
    public class RuleActionMapDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int AppRuleId { get; set; }
        public int AppActionId { get; set; }
        public int SortOrder { get; set; }    
    }
}
