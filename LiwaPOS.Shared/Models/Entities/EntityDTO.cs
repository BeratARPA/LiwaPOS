namespace LiwaPOS.Shared.Models.Entities
{
    public class EntityDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int EntityTypeId { get; set; }
        public string? Name { get; set; }
        public string? CustomData { get; set; }
    }
}
