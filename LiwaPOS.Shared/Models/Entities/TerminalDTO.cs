namespace LiwaPOS.Shared.Models.Entities
{
    public class TerminalDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public bool IsDefault { get; set; }
        public int ReportPrinterId { get; set; }
        public int TransactionPrinterId { get; set; }
    }
}
