namespace LiwaPOS.Entities.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? PinCode { get; set; }

        public int UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
