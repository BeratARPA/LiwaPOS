namespace LiwaPOS.Shared.Models.Entities
{
    public class MenuItemDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? GroupCode { get; set; }
        public string? Barcode { get; set; }
        public string? Tag { get; set; }
    }
}
