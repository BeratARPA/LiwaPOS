using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using System.Diagnostics;

namespace LiwaPOS.BLL.Actions
{
    public class RunProcessAction : IAction
    {
        public async Task Execute(string properties)
        {
            var runProcessProperties = JsonHelper.Deserialize<RunProcessDTO>(properties);
            if (runProcessProperties == null)
                return;

            var fileName = runProcessProperties.FileName;
            var arguments = runProcessProperties.Arguments;
            if (!string.IsNullOrEmpty(fileName))
            {
                var processStartInfo = new ProcessStartInfo(fileName, arguments);
                var isHidden = runProcessProperties.IsHidden;
                if ((bool)isHidden) processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                var useShellExecute = runProcessProperties.UseShellExecute;
                if ((bool)useShellExecute) processStartInfo.UseShellExecute = true;

                try
                {
                    Process.Start(processStartInfo);
                }
                catch { }
            }
        }
    }
}
