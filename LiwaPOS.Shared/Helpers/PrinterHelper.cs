using System.Drawing.Printing;
using System.Management;

namespace LiwaPOS.Shared.Helpers
{
    public class PrinterHelper
    {
        /// <summary>
        /// Tüm yazıcıları listeler.
        /// </summary>
        public static IEnumerable<string> GetPrinters()
        {
            List<string> printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }
            return printers;
        }

        /// <summary>
        /// Varsayılan yazıcıyı döner.
        /// </summary>
        public static string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            return settings.PrinterName;
        }

        /// <summary>
        /// Varsayılan yazıcıyı ayarlar.
        /// </summary>
        public static bool SetDefaultPrinter(string printerName)
        {
            try
            {
                using (var managementObject = new ManagementObject($"Win32_Printer.DeviceID='\\\\{Environment.MachineName}\\{printerName}'"))
                {
                    managementObject.InvokeMethod("SetDefaultPrinter", null);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting default printer: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Yazıcının durumunu döner.
        /// </summary>
        public static string GetPrinterStatus(string printerName)
        {
            try
            {
                string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerName.Replace("\\", "\\\\")}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        return printer["PrinterStatus"]?.ToString() ?? "Unknown";
                    }
                }
                return "Printer not found";
            }
            catch (Exception ex)
            {
                return $"Error retrieving printer status: {ex.Message}";
            }
        }
    }
}
