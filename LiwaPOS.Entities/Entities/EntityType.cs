namespace LiwaPOS.Entities.Entities
{
    public class EntityType : BaseEntity
    {
        public string? Name { get; set; }
        public string? EntityName { get; set; }
        public string? DefaultStates { get; set; }
        public string? DisplayFormat { get; set; }

        public ICollection<EntityCustomField>? EntityCustomFields { get; set; }
    }
}
