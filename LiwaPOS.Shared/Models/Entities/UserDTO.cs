namespace LiwaPOS.Shared.Models.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PinCode { get; set; }

        public virtual UserRoleDTO? UserRole { get; set; }
    }
}
