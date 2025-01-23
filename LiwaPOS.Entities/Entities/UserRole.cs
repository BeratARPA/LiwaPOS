namespace LiwaPOS.Entities.Entities
{
    public class UserRole : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
    }
}
