using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.Shared.Services
{
    public class LoggingService
    {
        private static readonly string _logDirectory = FolderLocationsHelper.LogsPath;
        private static readonly string _logFileName = $"log_{DateTime.UtcNow:yyyyMMdd}.log";

        private static async Task<string> GetLogFilePathAsync()
        {
            await DirectoryExtension.CreateIfNotExistsAsync(_logDirectory);
            return Path.Combine(_logDirectory, _logFileName);
        }

        public static async Task LogAsync(LogLevel level, string message, string source = null, string custom = null, Exception exception = null)
        {
            var logEntry = new LogEntry
            {
                Level = level,
                Message = message,
                Source = source,
                Custom = custom,
                Exception = exception?.Message,
                StackTrace = exception?.StackTrace
            };

            var logFilePath = await GetLogFilePathAsync();
            await FileExtension.AppendTextAsync(logFilePath, logEntry.ToString() + Environment.NewLine);
        }

        public static async Task LogTraceAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Trace, message, source, custom, exception);
        }

        public static async Task LogDebugAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Debug, message, source, custom, exception);
        }

        public static async Task LogInfoAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Info, message, source, custom, exception);
        }

        public static async Task LogWarnAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Warn, message, source, custom, exception);
        }

        public static async Task LogErrorAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Error, message, source, custom, exception);
        }

        public static async Task LogFatalAsync(string message, string source = null, string custom = null, Exception exception = null)
        {
            await LogAsync(LogLevel.Fatal, message, source, custom, exception);
        }
    }
}
