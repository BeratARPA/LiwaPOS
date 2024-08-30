using LiwaPOS.Shared.Enums;

namespace LiwaPOS.Shared.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public LogLevel Level { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public string? Custom { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] [{Source}] {Message} {Custom} {Exception} {StackTrace}";
        }
    }
}
