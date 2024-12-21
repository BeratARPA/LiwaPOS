namespace LiwaPOS.Shared.Models.Entities
{
    public class ScreenMenuCategoryDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Header { get; set; }
        public int ScreenMenuId { get; set; }
    }
}
