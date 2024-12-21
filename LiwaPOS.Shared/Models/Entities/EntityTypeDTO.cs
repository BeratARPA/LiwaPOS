namespace LiwaPOS.Shared.Models.Entities
{
    public class EntityTypeDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? EntityName { get; set; }
        public string? DefaultStates { get; set; }
        public string? DisplayFormat { get; set; }
    }
}
