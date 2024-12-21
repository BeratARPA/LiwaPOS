namespace LiwaPOS.Shared.Models.Entities
{
    public class RuleActionMapDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int TerminalId { get; set; }
        public int DepartmentId { get; set; }
        public int UserRoleId { get; set; }
        public int TicketTypeId { get; set; }
        public int AppRuleId { get; set; }
        public int AppActionId { get; set; }
        public int SortOrder { get; set; }
    }
}
