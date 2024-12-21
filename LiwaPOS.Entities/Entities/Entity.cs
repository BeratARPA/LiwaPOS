namespace LiwaPOS.Entities.Entities
{
    public class Entity : BaseEntity
    {
        public int EntityTypeId { get; set; }
        public string? Name { get; set; }
        public string? CustomData { get; set; }
    }
}
