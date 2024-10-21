using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;

namespace LiwaPOS.Shared.Services
{
    public class ConnectionService
    {
        private static readonly string _connectionStringFilePath = Path.Combine(FolderLocationsHelper.ConfigurationsPath, "ConnectionStrings.json");

        public async static Task SaveConnectionString(string connectionString)
        {
            await FileExtension.WriteTextAsync(_connectionStringFilePath, JsonHelper.Serialize(new { ConnectionString = connectionString }));
        }

        public static string GetConnectionString()
        {
            if (!File.Exists(_connectionStringFilePath))
                return Defaults.DefaultConnectionString;

            var fileContent = FileExtension.ReadText(_connectionStringFilePath);
            var connectionData =  JsonHelper.Deserialize<dynamic>(fileContent);
            return connectionData?.ConnectionString + " TrustServerCertificate=True; MultipleActiveResultSets=True;";
        }
    }
}
