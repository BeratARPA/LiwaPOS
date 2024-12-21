namespace LiwaPOS.Shared.Models.Entities
{
    public class MenuItemPriceDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int MenuItemPortionId { get; set; }
        public double Price { get; set; }
    }
}
