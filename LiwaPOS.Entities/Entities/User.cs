namespace LiwaPOS.Entities.Entities
{
    public class User : BaseEntity
    {
        public int UserRoleId { get; set; }
        public string? Name { get; set; }
        public string? PinCode { get; set; }
    }
}
