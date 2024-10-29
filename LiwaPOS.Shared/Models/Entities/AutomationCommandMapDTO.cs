namespace LiwaPOS.Shared.Models.Entities
{
    public class AutomationCommandMapDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int TerminalId { get; set; }
        public int DepartmentId { get; set; }
        public int UserRoleId { get; set; }
        public int TicketTypeId { get; set; }
        public int AutomationCommandId { get; set; }
    }
}
