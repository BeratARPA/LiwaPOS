using LiwaPOS.Entities.Enums;

namespace LiwaPOS.Entities.DTOs
{
    public class AppActionDTO
    {
        public int Id { get; set; }
        public string? ActionName { get; set; }
        public ActionType ActionType { get; set; }
        public string? Properties { get; set; }
    }
}
