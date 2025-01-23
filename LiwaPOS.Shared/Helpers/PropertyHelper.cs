namespace LiwaPOS.Shared.Helpers
{
    public class PropertyHelper
    {
        public static List<string> GetDataObjectProperties(Type dataObjectType)
        {
            return dataObjectType
                .GetProperties()
                .Select(p => p.Name)
                .ToList();
        }

        public static object GetData(object dataObject, string propertyName)
        {
            var propertyInfo = dataObject.GetType().GetProperty(propertyName);
            return propertyInfo?.GetValue(dataObject);
        }
    }
}
