using LiwaPOS.Entities.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class AppAction : BaseEntity
    {     
        public string? ActionName { get; set; }
        public ActionType ActionType { get; set; }
        public string? Properties { get; set; }
    }
}
