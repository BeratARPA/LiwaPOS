namespace LiwaPOS.Entities.Entities
{
    public class Permission : BaseEntity
    {
        public int UserRoleId { get; set; }
        public string? Name { get; set; }
        public bool Value { get; set; }
    }
}
