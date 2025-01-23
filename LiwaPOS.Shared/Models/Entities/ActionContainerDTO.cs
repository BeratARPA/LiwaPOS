namespace LiwaPOS.Shared.Models.Entities
{
    public class ActionContainerDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int AppActionId { get; set; }
        public int AppRuleId { get; set; }
        public string? Name { get; set; }
        public string? ParameterValues { get; set; }
        public string? CustomConstraint { get; set; }
        public bool IsAsync { get; set; }
    }
}
