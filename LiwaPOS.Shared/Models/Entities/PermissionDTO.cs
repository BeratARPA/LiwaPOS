namespace LiwaPOS.Shared.Models.Entities
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int UserRoleId { get; set; }
        public string? Name { get; set; }
        public bool Value { get; set; }
    }
}
