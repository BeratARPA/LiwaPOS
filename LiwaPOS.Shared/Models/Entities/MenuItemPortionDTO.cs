namespace LiwaPOS.Shared.Models.Entities
{
    public class MenuItemPortionDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public int MenuItemId { get; set; }
        public int Multiplier { get; set; }
    }
}
