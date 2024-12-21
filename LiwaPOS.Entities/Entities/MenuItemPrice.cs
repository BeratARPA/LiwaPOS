namespace LiwaPOS.Entities.Entities
{
    public class MenuItemPrice : BaseEntity
    {
        public int MenuItemPortionId { get; set; }
        public double Price { get; set; }
    }
}
