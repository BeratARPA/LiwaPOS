namespace LiwaPOS.Shared.Models.Entities
{
    public class ScreenMenuItemDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Header { get; set; }
        public int ScreenMenuCategoryId { get; set; }
        public int MenuItemId { get; set; }
    }
}
