namespace LiwaPOS.Shared.Models
{
    public class SendHttpRequestDTO
    {
        public string? Method { get; set; } = "";
        public string? Url { get; set; } = "";
        public string? Data { get; set; } = "";
        public string? Accept { get; set; } = "";
    }
}
