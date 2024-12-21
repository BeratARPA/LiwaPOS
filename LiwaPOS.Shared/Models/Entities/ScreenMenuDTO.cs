namespace LiwaPOS.Shared.Models.Entities
{
    public class ScreenMenuDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public int CategoryColumnCount { get; set; }
        public int CategoryColumnWidthRate { get; set; }
    }
}
