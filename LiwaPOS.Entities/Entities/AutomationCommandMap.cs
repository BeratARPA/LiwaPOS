namespace LiwaPOS.Entities.Entities
{
    public class AutomationCommandMap : BaseEntity
    {
        public int TerminalId { get; set; }
        public int DepartmentId { get; set; }
        public int UserRoleId { get; set; }
        public int TicketTypeId { get; set; }
        public int AutomationCommandId { get; set; }
        public string? DisplayOn { get; set; }
        public string? EnabledStates { get; set; }
        public string? VisibleStates { get; set; }
    }
}
