namespace LiwaPOS.Shared.Helpers
{
    public static class StreamHelper
    {
        public static async Task<Stream> OpenFileReadStreamAsync(string filePath)
        {
            return await Task.Run(() => new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true));
        }

        public static async Task<Stream> OpenFileWriteStreamAsync(string filePath)
        {
            return await Task.Run(() => new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true));
        }
    }
}
