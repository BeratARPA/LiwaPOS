using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.Shared.Services
{
    public class ConnectionDataDTO
    {
        public string? ConnectionString { get; set; }
    }

    public class ConnectionService
    {
        private static readonly string _connectionStringFilePath = Path.Combine(FolderLocationsHelper.ConfigurationsPath, "ConnectionStrings.json");

        public async static Task SaveConnectionString(string connectionString)
        {
            await FileExtension.WriteTextAsync(_connectionStringFilePath, JsonHelper.Serialize(new { ConnectionString = connectionString }));
        }

        public static bool IsSQLLocalDB(string connectionString)
        {
            return connectionString.Contains("Data Source=(localdb)");
        }

        public static string GetFullConnectionString()
        {
            if (!FileExtension.Exists(_connectionStringFilePath))
                return Defaults.DefaultConnectionString + " TrustServerCertificate=True; MultipleActiveResultSets=True;";

            var fileContent = FileExtension.ReadText(_connectionStringFilePath);
            var connectionData = JsonHelper.Deserialize<ConnectionDataDTO>(fileContent);

            if (string.IsNullOrEmpty(connectionData.ConnectionString))
                return Defaults.DefaultConnectionString + " TrustServerCertificate=True; MultipleActiveResultSets=True;";

            return connectionData?.ConnectionString + " TrustServerCertificate=True; MultipleActiveResultSets=True;";
        }

        public static string GetConnectionString()
        {
            if (!FileExtension.Exists(_connectionStringFilePath))
                return "";

            var fileContent = FileExtension.ReadText(_connectionStringFilePath);
            var connectionData = JsonHelper.Deserialize<ConnectionDataDTO>(fileContent);

            if (string.IsNullOrEmpty(connectionData.ConnectionString))
                return "";

            return connectionData?.ConnectionString;
        }

        public static string ModelToString(ConnectionStringDTO connectionString)
        {
            return @$"Data Source={connectionString.DataSource}; User Id={connectionString.UserId}; Password={connectionString.Password}; Database={connectionString.Database};";
        }
    }
}
