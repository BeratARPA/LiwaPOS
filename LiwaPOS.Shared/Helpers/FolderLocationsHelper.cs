namespace LiwaPOS.Shared.Helpers
{
    public class FolderLocationsHelper
    {
        public static string LiwaPOSProgramDataPath => @"C:\ProgramData\LiwaPOS";
        public static string LogsPath => @"C:\ProgramData\LiwaPOS\Logs";
        public static string ConfigurationsPath => @"C:\ProgramData\LiwaPOS\Configurations";
        public static string LocalizationPath => @$"{AppDomain.CurrentDomain.BaseDirectory}\Resources\Localization";
        public static string LanguagesPath => @$"{AppDomain.CurrentDomain.BaseDirectory}\Resources\Localization\Languages";
    }
}
