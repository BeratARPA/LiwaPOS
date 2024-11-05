using LiwaPOS.DAL.Services;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Services;

namespace LiwaPOS.BLL.Services
{
    public class DatabaseInitializerService
    {
        private readonly DatabaseInitializer _databaseInitializer;

        public DatabaseInitializerService(DatabaseInitializer databaseInitializer)
        {
            _databaseInitializer = databaseInitializer;
        }

        public async Task Initialize()
        {
            string connectionString = ConnectionService.GetFullConnectionString();
            if (ConnectionService.IsSQLLocalDB(connectionString))
            {
                if (!SqlServerInstallerHelper.IsSqlExpressInstalled())
                    await SqlServerInstallerHelper.InstallSqlExpress();
            }

            _databaseInitializer.Initialize();
        }
    }
}
