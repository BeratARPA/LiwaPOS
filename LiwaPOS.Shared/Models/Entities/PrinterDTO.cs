namespace LiwaPOS.Shared.Models.Entities
{
    public class PrinterDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? ShareName { get; set; }
        public bool RTLMode { get; set; }
        public string? CharReplacement { get; set; }
    }
}
