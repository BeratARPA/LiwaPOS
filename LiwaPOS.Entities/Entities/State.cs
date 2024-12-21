using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class State : BaseEntity
    {
        public string? Name { get; set; }
        public string? GroupName { get; set; }
        public StateType StateTypeId { get; set; }
        public string? Color { get; set; }
    }
}
