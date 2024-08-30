using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Entities.DTOs
{
    public class AppActionDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ActionType Type { get; set; }
        public string? Properties { get; set; }
    }
}
