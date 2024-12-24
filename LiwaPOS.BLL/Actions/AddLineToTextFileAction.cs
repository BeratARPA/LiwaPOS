using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class AddLineToTextFileAction : IAction
    {
        public async Task<object> Execute(string properties)
        {
            var addLineToTextFileProperties = JsonHelper.Deserialize<AddLineToTextFileDTO>(properties);
            if (addLineToTextFileProperties == null)
                return false;

            var filePath = addLineToTextFileProperties.FilePath;
            var text = addLineToTextFileProperties.Text;
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                await File.AppendAllTextAsync(filePath, text + Environment.NewLine);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
