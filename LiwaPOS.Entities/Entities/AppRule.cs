using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class AppRule : BaseEntity
    {
        public string? Name { get; set; }
        public string? Constraints { get; set; }
        public ConditionMatchType? ConstraintMatch { get; set; }
        public EventType? Type { get; set; }
    }
}
