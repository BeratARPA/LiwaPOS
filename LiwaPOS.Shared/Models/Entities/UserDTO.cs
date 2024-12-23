namespace LiwaPOS.Shared.Models.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int UserRoleId { get; set; }
        public string? Name { get; set; }
        public string? PinCode { get; set; }
    }
}
