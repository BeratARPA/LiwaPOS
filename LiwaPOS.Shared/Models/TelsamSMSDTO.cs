namespace LiwaPOS.Shared.Models
{
    public class TelsamSmsDTO
    {
        public string? Username { get; set; } = "";
        public string? Password { get; set; } = "";
        public string? Message { get; set; } = "";
        public string? Title { get; set; } = "";
        public string? ToPhoneNumber { get; set; } = "";
        public int? AHS { get; set; } = 0;
        public int? NLSS { get; set; } = 0;
    }
}
