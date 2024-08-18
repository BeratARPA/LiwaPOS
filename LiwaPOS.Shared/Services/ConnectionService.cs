using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;

namespace LiwaPOS.Shared.Services
{
    public class ConnectionService
    {
        private static readonly string _connectionStringFilePath = Path.Combine(FolderLocations.ConfigurationsPath, "ConnectionStrings.json");

        public async static Task SaveConnectionString(string connectionString)
        {
            await FileExtension.WriteTextAsync(_connectionStringFilePath, await JsonHelper.SerializeAsync(new { ConnectionString = connectionString }));
        }

        public async static Task<string> GetConnectionString()
        {
            if (!File.Exists(_connectionStringFilePath))
                return Defaults.DefaultConnectionString;

            var fileContent = FileExtension.ReadTextAsync(_connectionStringFilePath);
            var connectionData =  await JsonHelper.DeserializeAsync<dynamic>(fileContent);
            return connectionData?.ConnectionString;
        }
    }
}
