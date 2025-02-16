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
            return PrinterSettings.InstalledPrinters.Cast<string>();
        }

        /// <summary>
        /// Belirtilen yazıcıyı getirir.
        /// </summary>
        public static PrinterSettings GetPrinter(string printerName)
        {
            var printers = GetPrinters();
            if (printers.Contains(printerName))
            {
                return new PrinterSettings { PrinterName = printerName };
            }
            return null;
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

        /// <summary>
        /// Yazıcının kağıt durumunu kontrol eder.
        /// </summary>
        public static string GetPrinterPaperStatus(string printerName)
        {
            try
            {
                string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerName.Replace("\\", "\\\\")}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        int extendedPrinterStatus = Convert.ToInt32(printer["ExtendedPrinterStatus"]);
                        return extendedPrinterStatus switch
                        {
                            1 => "Other",
                            2 => "Unknown",
                            3 => "Idle",
                            4 => "Printing",
                            5 => "Warmup",
                            6 => "Stopped Printing",
                            7 => "Offline",
                            _ => "Unknown Status",
                        };
                    }
                }
                return "Printer Not Found";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Yazıcıya test sayfası gönderir.
        /// </summary>
        public static bool PrintTestPage(string printerName)
        {
            try
            {
                string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        printer.InvokeMethod("PrintTestPage", null);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test page error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Yazıcı bağlantısını kaldırır.
        /// </summary>
        public static bool RemovePrinter(string printerName)
        {
            try
            {
                string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        printer.Delete();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove printer error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Yazıcı kuyruğunu duraklatır.
        /// </summary>
        public static bool PausePrinter(string printerName)
        {
            return ChangePrinterState(printerName, "Pause");
        }

        /// <summary>
        /// Yazıcı kuyruğunu devam ettirir.
        /// </summary>
        public static bool ResumePrinter(string printerName)
        {
            return ChangePrinterState(printerName, "Resume");
        }

        private static bool ChangePrinterState(string printerName, string action)
        {
            try
            {
                string query = $"SELECT * FROM Win32_Printer WHERE Name = '{printerName.Replace("\\", "\\\\")}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        printer.InvokeMethod(action, null);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{action} error: {ex.Message}");
                return false;
            }
        }
    }
}
