namespace LiwaPOS.Entities.Entities
{
    public class MenuItemPortion : BaseEntity
    {
        public string? Name { get; set; }
        public int MenuItemId { get; set; }
        public int Multiplier { get; set; }

        public ICollection<MenuItemPrice>? MenuItemPrices { get; set; }
    }
}
