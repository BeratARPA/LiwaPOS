namespace LiwaPOS.Shared.Models.Entities
{
    public class PrinterMapDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int DepartmentId { get; set; }
        public int TerminalId { get; set; }
        public int TicketTypeId { get; set; }
        public int PrintJobId { get; set; }
        public int PrinterId { get; set; }
        public int PrinterTemplateId { get; set; }
        public string? ProductTags { get; set; }
        public string? MenuItemGroupCode { get; set; }
        public int MenuItemId { get; set; }
    }
}
