namespace LiwaPOS.Entities.Entities
{
    public class MenuItemPrice : BaseEntity
    {
        public int MenuItemPortionId { get; set; }
        public string? PriceTag { get; set; }
        public double Price { get; set; }
    }
}
