using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class AppAction : BaseEntity
    {     
        public string? Name { get; set; }
        public ActionType Type { get; set; }
        public string? Properties { get; set; }
    }
}
