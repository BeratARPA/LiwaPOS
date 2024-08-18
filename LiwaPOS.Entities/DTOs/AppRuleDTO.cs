namespace LiwaPOS.Entities.DTOs
{
    public class AppRuleDTO
    {
        public int Id { get; set; }
        public string? RuleName { get; set; }
        public string? EventName { get; set; }

        public virtual List<AppActionDTO>? Actions { get; set; }
    }
}
