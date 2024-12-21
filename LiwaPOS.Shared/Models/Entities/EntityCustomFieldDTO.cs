using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models.Entities
{
    public class EntityCustomFieldDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public int EntityTypeId { get; set; }
        public string? Name { get; set; }
        public FieldType FieldTypeId { get; set; }
        public MaskType MaskTypeId { get; set; }
        public string? EditingFormat { get; set; }
        public bool Hidden { get; set; }
        public string? DefaultValue { get; set; }
    }
}
