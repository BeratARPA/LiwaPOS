using LiwaPOS.Shared.Extensions;
using System.Text.Json;

namespace LiwaPOS.Shared.Helpers
{
    public static class JsonHelper
    {
        // JSON verisini serileştirme
        public static async Task<string> SerializeAsync<T>(T data, JsonSerializerOptions options = null)
        {
            return await Task.Run(() => JsonSerializer.Serialize(data, options));
        }

        // JSON verisini dosyaya yazma
        public static async Task SerializeToFileAsync<T>(string filePath, T data, JsonSerializerOptions options = null)
        {
            var json = await SerializeAsync(data, options);
            await FileExtension.WriteTextAsync(filePath, json);
        }

        // JSON verisini deserileştirme
        public static async Task<T> DeserializeAsync<T>(string json, JsonSerializerOptions options = null)
        {
            return await Task.Run(() => JsonSerializer.Deserialize<T>(json, options));
        }

        // Dosyadan JSON verisini okuma ve deserileştirme
        public static async Task<T> DeserializeFromFileAsync<T>(string filePath, JsonSerializerOptions options = null)
        {
            var json = FileExtension.ReadTextAsync(filePath);
            return await DeserializeAsync<T>(json, options);
        }

        // JSON verisini formatlama
        public static async Task<string> FormatJsonAsync(string json)
        {
            var jsonElement = await Task.Run(() => JsonSerializer.Deserialize<JsonElement>(json));
            return await Task.Run(() => JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
