namespace LiwaPOS.Shared.Models.Entities
{
    public class ProgramSettingValueDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
