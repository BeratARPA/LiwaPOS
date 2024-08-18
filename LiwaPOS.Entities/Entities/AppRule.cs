namespace LiwaPOS.Entities.Entities
{
    public class AppRule : BaseEntity
    {
        public string? Name { get; set; }
        public string? EventName { get; set; }

        public virtual ICollection<AppAction>? Actions { get; set; }
    }
}
