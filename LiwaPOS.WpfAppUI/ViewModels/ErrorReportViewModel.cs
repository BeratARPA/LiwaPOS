using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Services;
using LiwaPOS.WpfAppUI.Commands;
using LiwaPOS.WpfAppUI.Extensions;
using System.Management;
using System.Text;
using System.Windows.Input;

namespace LiwaPOS.WpfAppUI.ViewModels
{
    public class ErrorReportViewModel : ViewModelBase
    {
        private readonly Exception _exception;
        private string _errorMessage;
        private string _userMessage;
        private string _errorReportAsText;

        public string ErrorMessage { get { return _exception.Message; } }

        public string UserMessage
        {
            get => _userMessage;
            set
            {
                _userMessage = value;
                OnPropertyChanged();
            }
        }

        public string ErrorReportAsText
        {
            get { return _errorReportAsText ?? (_errorReportAsText = GenerateErrorReport()); }
            set { _errorReportAsText = value; }
        }

        public ICommand CopyCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SubmitCommand { get; }

        public ErrorReportViewModel(Exception exception)
        {
            _exception = exception;

            CopyCommand = new RelayCommand(CopyReport);
            SaveCommand = new AsyncRelayCommand(async _ => await SaveReportAsync());
            SubmitCommand = new AsyncRelayCommand(SubmitReportAsync);
        }

        private string GenerateErrorReport()
        {
            var sb = new StringBuilder();

            // Sistem Bilgileri
            sb.AppendLine("[System Info]")
                .AppendLine($"Machine Name: {Environment.MachineName}")
                .AppendLine($"User Name: {Environment.UserName}")
                .AppendLine($"OS Version: {Environment.OSVersion.VersionString}")
                .AppendLine($"CLR Version: {Environment.Version}")
                .AppendLine($"Processor Count: {Environment.ProcessorCount}")
                .AppendLine($"Available Memory: {GetAvailableMemory()} MB")
                .AppendLine($"Is 64-Bit OS: {Environment.Is64BitOperatingSystem}")
                .AppendLine($"Is 64-Bit Process: {Environment.Is64BitProcess}")
                .AppendLine();

            // Donanım Bilgileri
            sb.AppendLine("[Hardware Info]")
                .AppendLine($"CPU: {GetCPUInfo()}")
                .AppendLine($"RAM: {GetRAMInfo()}")
                .AppendLine($"Graphics: {GetGraphicsInfo()}")
                .AppendLine($"Motherboard: {GetMotherboardInfo()}")
                .AppendLine();

            // Hard Disk Bilgileri
            sb.AppendLine("[Hard Disk Info]")
                .AppendLine(GetHardDiskInfo());

            // İşletim Sistemi Bilgileri
            sb.AppendLine("[OS Info]")
                .AppendLine($"OS Build: {GetOSBuildInfo()}")
                .AppendLine($"OS Architecture: {GetOSArchitecture()}")
                .AppendLine($"System Directory: {Environment.SystemDirectory}")
                .AppendLine($"Windows Version: {GetWindowsVersion()}")
                .AppendLine();

            // Uygulama Bilgileri
            sb.AppendLine("[Application Info]")
                .AppendLine($"Application: {Application.ProductName}")
                .AppendLine($"Version: {Application.ProductVersion}")
                .AppendLine($"App Path: {Application.ExecutablePath}")
                .AppendLine();

            // İstisna Bilgileri
            sb.AppendLine("[Exception Info]");
            AppendExceptionInfo(sb, _exception);

            return sb.ToString();
        }

        private void AppendExceptionInfo(StringBuilder sb, Exception ex, int level = 0)
        {
            if (ex == null) return;

            sb.AppendLine($"Exception Level: {level}")
                .AppendLine($"Type: {ex.GetType()}")
                .AppendLine($"Message: {ex.Message}")
                .AppendLine($"Source: {ex.Source}")
                .AppendLine($"Stack Trace: {ex.StackTrace}")
                .AppendLine($"Target Site: {ex.TargetSite}")
                .AppendLine($"Help Link: {ex.HelpLink}")
                .AppendLine();

            foreach (var key in ex.Data.Keys)
            {
                sb.AppendLine($"Data - Key: {key}, Value: {ex.Data[key]}");
            }

            AppendExceptionInfo(sb, ex.InnerException, level + 1);
        }

        private string GetAvailableMemory()
        {
            var memoryStatus = new Microsoft.VisualBasic.Devices.ComputerInfo();
            return (memoryStatus.AvailablePhysicalMemory / (1024 * 1024)).ToString();
        }

        private string GetCPUInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["Name"].ToString();
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetRAMInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return $"{Convert.ToInt64(obj["TotalPhysicalMemory"]) / (1024 * 1024)} MB";
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetGraphicsInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["Name"].ToString();
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetMotherboardInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["Product"].ToString();
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetHardDiskInfo()
        {
            try
            {
                var sb = new StringBuilder();
                using (var searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        sb.AppendLine($"Model: {obj["Model"]}");
                        sb.AppendLine($"Interface Type: {obj["InterfaceType"]}");
                        sb.AppendLine($"Size: {Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024)} GB");
                        sb.AppendLine($"Serial Number: {GetDiskSerialNumber(obj["DeviceID"].ToString())}");
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return "N/A";
            }
        }

        private string GetDiskSerialNumber(string deviceId)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PhysicalMedia WHERE Tag='{deviceId}'"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["SerialNumber"]?.ToString().Trim();
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetOSBuildInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["BuildNumber"].ToString();
                    }
                }
            }
            catch
            {
                return "N/A";
            }
            return "N/A";
        }

        private string GetOSArchitecture()
        {
            return Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
        }

        private string GetWindowsVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        private void CopyReport(object parameter)
        {
            // Hata raporunu panoya kopyala
            Clipboard.SetText(ErrorReportAsText);
        }

        private async Task SubmitReportAsync(object parameter)
        {
            // Hata raporunu sunucuya gönder
            //await Task.Run(() => WebService.SubmitErrorReport(ErrorMessage, UserMessage, ErrorReportAsText));
        }

        private async Task SaveReportAsync()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                DefaultExt = "txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;

                    await FileExtension.WriteTextAsync(saveFileDialog.FileName, ErrorReportAsText);
                }
                catch (Exception exception)
                {
                    await LoggingService.LogErrorAsync(string.Format("Unable to save file '{0}' : {1}", saveFileDialog.FileName, exception.Message), "ErrorReportViewModel", "", exception);
                    MessageBox.Show(string.Format(await TranslatorExtension.TranslateUI("UnableToSaveFile_ph"), saveFileDialog.FileName, exception.Message));
                }
            }
        }        
    }
}
