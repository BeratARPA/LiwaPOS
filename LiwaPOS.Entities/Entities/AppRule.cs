using LiwaPOS.Entities.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class AppRule : BaseEntity
    {
        public string? Name { get; set; }
        public EventType? Type { get; set; }
    }
}
