namespace LiwaPOS.Shared.Models.Entities
{
    public class WarehouseDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public int WarehouseTypeId { get; set; }
    }
}
