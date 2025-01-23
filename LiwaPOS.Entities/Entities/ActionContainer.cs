namespace LiwaPOS.Entities.Entities
{
    public class ActionContainer : BaseEntity
    {
        public int AppActionId { get; set; }
        public int AppRuleId { get; set; }
        public string? Name { get; set; }
        public string? ParameterValues { get; set; }
        public string? CustomConstraint { get; set; }
        public bool IsAsync { get; set; }
    }
}
