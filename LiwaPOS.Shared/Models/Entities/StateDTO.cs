using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models.Entities
{
    public class StateDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? GroupName { get; set; }
        public StateType StateTypeId { get; set; }
        public string? Color { get; set; }
    }
}
