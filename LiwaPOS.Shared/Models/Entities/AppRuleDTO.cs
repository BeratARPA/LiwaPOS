using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models.Entities
{
    public class AppRuleDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Constraints { get; set; }
        public ConditionMatchType ConditionMatchTypeId { get; set; }
        public EventType EventTypeId { get; set; }
    }
}
