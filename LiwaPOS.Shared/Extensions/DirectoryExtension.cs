namespace LiwaPOS.Shared.Extensions
{
    public static class DirectoryExtension
    {
        public static async Task CreateIfNotExistsAsync(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                await Task.Run(() => Directory.CreateDirectory(directoryPath));
            }
        }

        public static async Task<bool> DeleteAsync(string directoryPath, bool recursive = true)
        {
            if (Directory.Exists(directoryPath))
            {
                await Task.Run(() => Directory.Delete(directoryPath, recursive));
                return true;
            }
            return false;
        }

        public static async Task<IEnumerable<string>> GetFilesAsync(string directoryPath, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (Directory.Exists(directoryPath))
            {
                return await Task.Run(() => Directory.EnumerateFiles(directoryPath, searchPattern, searchOption).ToList());
            }
            return Enumerable.Empty<string>();
        }

        public static async Task<IEnumerable<string>> GetDirectoriesAsync(string directoryPath, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (Directory.Exists(directoryPath))
            {
                return await Task.Run(() => Directory.EnumerateDirectories(directoryPath, searchPattern, searchOption).ToList());
            }
            return Enumerable.Empty<string>();
        }

        public static async Task<long> GetDirectorySizeAsync(string directoryPath, bool includeSubdirectories = true)
        {
            var files = await GetFilesAsync(directoryPath, "*.*", includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            long totalSize = 0;

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                totalSize += fileInfo.Length;
            }

            return totalSize;
        }

        public static async Task CopyDirectoryAsync(string sourceDir, string destDir, bool overwrite = true)
        {
            await CreateIfNotExistsAsync(destDir);

            var files = await GetFilesAsync(sourceDir);
            foreach (var file in files)
            {
                var destFile = Path.Combine(destDir, Path.GetFileName(file));
                await FileExtension.CopyFileAsync(file, destFile, overwrite);
            }

            var directories = await GetDirectoriesAsync(sourceDir);
            foreach (var directory in directories)
            {
                var destSubDir = Path.Combine(destDir, Path.GetFileName(directory));
                await CopyDirectoryAsync(directory, destSubDir, overwrite);
            }
        }

        public static async Task MoveDirectoryAsync(string sourceDir, string destDir, bool overwrite = true)
        {
            await CopyDirectoryAsync(sourceDir, destDir, overwrite);
            await DeleteAsync(sourceDir, true);
        }
    }
}
