namespace LiwaPOS.Shared.Models.Entities
{
    public class WarehouseTypeDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public bool Hidden { get; set; }
    }
}
