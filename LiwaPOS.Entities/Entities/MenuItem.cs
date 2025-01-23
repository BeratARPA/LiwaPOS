namespace LiwaPOS.Entities.Entities
{
    public class MenuItem : BaseEntity
    {
        public string? Name { get; set; }
        public string? GroupCode { get; set; }
        public string? Barcode { get; set; }
        public string? Tag { get; set; }

        public ICollection<MenuItemPortion>? MenuItemPortions{ get; set; }
    }
}
