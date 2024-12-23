namespace LiwaPOS.Entities.Entities
{
    public class PrinterMap : BaseEntity
    {
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
