using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class AddLineToTextFileAction : IAction
    {
        public void Execute(string properties)
        {
            var addLineToTextFileProperties = JsonHelper.Deserialize<AddLineToTextFileDTO>(properties);
            if (addLineToTextFileProperties == null)
                return;

            var filePath = addLineToTextFileProperties.FilePath;
            var text = addLineToTextFileProperties.Text;
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.AppendAllText(filePath, text + Environment.NewLine);
            }
            catch { }
        }
    }
}
