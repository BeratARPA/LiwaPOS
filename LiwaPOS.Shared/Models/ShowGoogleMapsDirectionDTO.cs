namespace LiwaPOS.Shared.Models
{
    public class ShowGoogleMapsDirectionDTO
    {
        public string? APIKey { get; set; } = "";
        public string? OriginLat { get; set; } = "";
        public string? OriginLong { get; set; } = "";
        public string? DestinationLat { get; set; } = "";
        public string? DestinationLong { get; set; } = "";
    }
}
