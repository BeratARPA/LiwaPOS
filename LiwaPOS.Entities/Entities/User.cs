namespace LiwaPOS.Entities.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? PinCode { get; set; }

        public virtual UserRole? UserRole { get; set; }
    }
}
