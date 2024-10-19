using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models.Entities
{
    public class AppRuleDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Constraints { get; set; }
        public ConditionMatchType ConditionMatch { get; set; }
        public EventType Type { get; set; }
    }
}
