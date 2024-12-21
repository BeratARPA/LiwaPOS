namespace LiwaPOS.Shared.Models.Entities
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int TerminalId { get; set; }
        public int DepartmentId { get; set; }
        public string? Note { get; set; }
        public string? TicketTags { get; set; }
        public string? TicketStates { get; set; }
        public string? TicketLogs { get; set; }
        public double ExchangeRate { get; set; }
        public string? Name { get; set; }
        public bool IsOpened { get; set; }
        public bool IsClosed { get; set; }
        public bool IsLocked { get; set; }
        public int TicketTypeId { get; set; }
        public string? TicketNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastOrderDateTime { get; set; }
        public DateTime LastPaymentDateTime { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string? LastUpdateUsername { get; set; }
        public string? CreatedUsername { get; set; }
        public double RemainingAmount { get; set; }
        public double TotalAmount { get; set; }
    }
}
