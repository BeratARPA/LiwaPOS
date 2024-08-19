using LiwaPOS.Shared.Extensions;
using System.Text.Json;

namespace LiwaPOS.Shared.Helpers
{
    public static class JsonHelper
    {
        // JSON verisini serileştirme
        public static string Serialize<T>(T data, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(data, options);
        }

        // JSON verisini dosyaya yazma
        public static async Task SerializeToFileAsync<T>(string filePath, T data, JsonSerializerOptions options = null)
        {
            var json = Serialize(data, options);
            await FileExtension.WriteTextAsync(filePath, json);
        }

        // JSON verisini deserileştirme
        public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        // Dosyadan JSON verisini okuma ve deserileştirme
        public static T DeserializeFromFileAsync<T>(string filePath, JsonSerializerOptions options = null)
        {
            var json = FileExtension.ReadText(filePath);
            return Deserialize<T>(json, options);
        }

        // JSON verisini formatlama
        public static string FormatJson(string json)
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            return JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
