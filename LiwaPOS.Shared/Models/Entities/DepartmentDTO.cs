namespace LiwaPOS.Shared.Models.Entities
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public int WarehouseId { get; set; }
        public int ScreenMenuId { get; set; }
    }
}
