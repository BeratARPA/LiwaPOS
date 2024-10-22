using LiwaPOS.Shared.Extensions;
using System.Xml;
using System.Xml.Serialization;

namespace LiwaPOS.Shared.Helpers
{
    public static class XmlHelper
    {
        // XML verisini serileştirme
        public static string Serialize<T>(T data)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, data);
                    return stringWriter.ToString();
                }
            }
        }

        // XML verisini dosyaya yazma
        public static async Task SerializeToFileAsync<T>(string filePath, T data)
        {
            var xml = Serialize(data);
            await FileExtension.WriteTextAsync(filePath, xml);
        }

        // XML verisini deserileştirme
        public static T Deserialize<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        public static object Deserialize(string xml, Type type)
        {
            var xmlSerializer = new XmlSerializer(type);

            using (var stringReader = new StringReader(xml))
            {
                return xmlSerializer.Deserialize(stringReader);
            }
        }

        // Dosyadan XML verisini okuma ve deserileştirme
        public static T DeserializeFromFileAsync<T>(string filePath)
        {
            var xml = FileExtension.ReadText(filePath);
            return Deserialize<T>(xml);
        }

        // XML verisini formatlama
        public static string FormatXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                {
                    doc.WriteTo(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
        }
    }
}
