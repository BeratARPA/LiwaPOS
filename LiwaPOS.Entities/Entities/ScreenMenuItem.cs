namespace LiwaPOS.Entities.Entities
{
    public class ScreenMenuItem : BaseEntity
    {
        public string? Name { get; set; }
        public string? Header { get; set; }
        public int ScreenMenuCategoryId { get; set; }
        public int MenuItemId { get; set; }
    }
}
