using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models.Entities
{
    public class AppActionDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public ActionType ActionTypeId { get; set; }
        public string? Properties { get; set; }
    }
}
