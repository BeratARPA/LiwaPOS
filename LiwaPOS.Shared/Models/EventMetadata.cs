using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models
{
    public class EventMetadata
    {
        public EventType EventType { get; set; }
        public Type? DataObjectType { get; set; }
    }
}
