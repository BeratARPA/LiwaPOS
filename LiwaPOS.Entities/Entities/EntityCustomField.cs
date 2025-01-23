using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Entities.Entities
{
    public class EntityCustomField : BaseEntity
    {
        public string? Name { get; set; }
        public FieldType FieldTypeId { get; set; }
        public MaskType MaskTypeId { get; set; }
        public string? EditingFormat { get; set; }
        public bool Hidden { get; set; }
        public string? DefaultValue { get; set; }

        public int EntityTypeId { get; set; }
        public EntityType? EntityType{ get; set; }
    }
}
