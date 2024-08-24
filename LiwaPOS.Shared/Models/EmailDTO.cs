namespace LiwaPOS.Shared.Models
{
    public class EmailDTO
    {
        public string? SMTPServer { get; set; } = "";
        public string? SMTPUser { get; set; } = "";
        public string? SMTPPassword { get; set; } = "";
        public int? SMTPPort { get; set; } = 587;
        public bool? SMTPEnableSsl { get; set; } = false;
        public string? ToEmailAddress { get; set; } = "";
        public string? Subject { get; set; } = "";
        public string? Message { get; set; } = "";
        public string? CCEmailAddresses { get; set; } = "";
        public string? FromEmailAddress { get; set; } = "";
    }
}
