namespace LiwaPOS.Shared.Models
{
    public class RunProcessDTO
    {
        public string? FileName { get; set; } = "";
        public string? Arguments { get; set; } = "";
        public bool? IsHidden { get; set; } = false;
        public bool? UseShellExecute { get; set; } = false;
    }
}
