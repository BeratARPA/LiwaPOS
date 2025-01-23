namespace LiwaPOS.Entities.Entities
{
    public class PrintJob : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<PrinterMap>? PrinterMaps { get; set; }
    }
}
