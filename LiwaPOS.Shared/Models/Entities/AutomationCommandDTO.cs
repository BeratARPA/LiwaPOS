namespace LiwaPOS.Shared.Models.Entities
{
    public class AutomationCommandDTO
    {
        public int Id { get; set; }
        public Guid EntityGuid { get; set; }
        public string? Category { get; set; }
        public string? ButtonHeader { get; set; }
        public string? Color { get; set; }
        public int FontSize { get; set; }
        public string? Values { get; set; }
        public string? Image { get; set; }
        public bool ToggleValues { get; set; }
        public bool ExecuteOnce { get; set; }
        public bool ClearSelection { get; set; }
        public int ConfirmationType { get; set; }
        public string? Symbol { get; set; }
        public string? ContentTemplate { get; set; }
        public int AutoRefresh { get; set; }
        public int TileCacheLifetime { get; set; }
        public string? NavigationModule { get; set; }
        public bool AskTextInput { get; set; }
        public bool AskNumericInput { get; set; }
        public string? Name { get; set; }
    }
}
