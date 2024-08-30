namespace LiwaPOS.Shared.Models
{
    public class OpenWebsiteOnWindowDTO
    {
        public string? URL { get; set; } = "www.liwasoft.com";
        public bool? UseHttps { get; set; } = true;
        public int? Width { get; set; } = 800;
        public int? Height { get; set; } = 600;
        public string? Title { get; set; } = "LiwaSoft";
        public bool? UseBorder { get; set; } = true;
        public bool? UseFullscreen { get; set; } = false;
    }
}
