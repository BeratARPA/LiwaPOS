namespace LiwaPOS.Shared.Models.Entities
{
    public class PrinterTemplateDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Template { get; set; }
    }
}
