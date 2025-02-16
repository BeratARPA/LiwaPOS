namespace LiwaPOS.Entities.Entities
{
    public class Printer : BaseEntity
    {
        public string? Name { get; set; }
        public string? ShareName { get; set; }
        public bool RTLMode { get; set; }
        public string? CharReplacement { get; set; }
        public int LineCharactersCount { get; set; }
    }
}
