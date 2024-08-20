using LiwaPOS.Entities.Enums;

namespace LiwaPOS.Entities.DTOs
{
    public class AppRuleDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public EventType? Type { get; set; }
    }
}
