using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Services;

namespace LiwaPOS.Shared.Extensions
{
    public static class FileExtension
    {
        public static async Task<bool> DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return true;
            }
            return false;
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath) ? true : false;
        }

        public static async Task CopyFileAsync(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            var destDirectory = Path.GetDirectoryName(destFilePath);
            if (!string.IsNullOrEmpty(destDirectory))
            {
                await DirectoryExtension.CreateIfNotExistsAsync(destDirectory);
            }

            await Task.Run(() => File.Copy(sourceFilePath, destFilePath, overwrite));
        }

        public static async Task MoveFileAsync(string sourceFilePath, string destFilePath, bool overwrite = true)
        {
            await CopyFileAsync(sourceFilePath, destFilePath, overwrite);
            await DeleteFileAsync(sourceFilePath);
        }

        public static byte[] ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            return new byte[0];
        }

        public static string ReadText(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }

        public static async Task WriteTextAsync(string filePath, string content)
        {
            var destDirectory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(destDirectory))
            {
                await DirectoryExtension.CreateIfNotExistsAsync(destDirectory);
            }

            File.WriteAllText(filePath, content);
        }

        public static async Task AppendTextAsync(string filePath, string content)
        {
            var destDirectory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(destDirectory))
            {
                await DirectoryExtension.CreateIfNotExistsAsync(destDirectory);
            }

            File.AppendAllText(filePath, content);
        }

        public static async Task<Stream> OpenReadStreamAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                return await StreamHelper.OpenFileReadStreamAsync(filePath);
            }
            await LoggingService.LogErrorAsync("File not found.", typeof(FileExtension).Name, filePath, new FileNotFoundException());
            return null;
        }

        public static async Task<Stream> OpenWriteStreamAsync(string filePath)
        {
            var destDirectory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(destDirectory))
            {
                await DirectoryExtension.CreateIfNotExistsAsync(destDirectory);
            }

            return await StreamHelper.OpenFileWriteStreamAsync(filePath);
        }
    }
}
