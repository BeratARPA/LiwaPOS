using Microsoft.Win32;
using System.Diagnostics;

namespace LiwaPOS.Shared.Helpers
{
    public static class SqlServerInstallerHelper
    {
        public static bool IsSqlExpressInstalled()
        {
            // SQL Server Express'in yüklü olup olmadığını kontrol et
            var registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server Local DB");
            return registryKey != null;
        }

        public static async Task InstallSqlExpress()
        {
            if (File.Exists(FolderLocationsHelper.SqlLocalDBPath))
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = "msiexec.exe",
                    Arguments = $"/i \"{FolderLocationsHelper.SqlLocalDBPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                using (var process = Process.Start(processInfo))
                {
                    await process.WaitForExitAsync();
                }
            }
            else
            {
                throw new FileNotFoundException("SQL Server Express yükleyici dosyası bulunamadı.");
            }
        }
    }
}
