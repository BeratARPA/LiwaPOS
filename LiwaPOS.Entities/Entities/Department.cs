namespace LiwaPOS.Entities.Entities
{
    public class Department : BaseEntity
    {
        public string? Name { get; set; }
        public int WarehouseId { get; set; }
        public int ScreenMenuId { get; set; }
    }
}
