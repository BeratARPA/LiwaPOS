namespace LiwaPOS.Entities.Entities
{
    public class Terminal : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsDefault { get; set; }
        public int ReportPrinterId { get; set; }
        public int TransactionPrinterId { get; set; }
    }
}
