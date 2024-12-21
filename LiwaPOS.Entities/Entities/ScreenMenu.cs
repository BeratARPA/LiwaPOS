namespace LiwaPOS.Entities.Entities
{
    public class ScreenMenu : BaseEntity
    {
        public string? Name { get; set; }
        public int CategoryColumnCount { get; set; }
        public int CategoryColumnWidthRate { get; set; }
    }
}
