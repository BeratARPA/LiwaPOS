using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using System.Text.RegularExpressions;

namespace LiwaPOS.Shared.Models
{
    public class RuleConstraintDTO
    {  
        public string? Name { get; set; }  
        public string? Left { get; set; } 
        public string? Operation { get; set; }  
        public string? Right { get; set; }

        public bool Satisfies(object dataObject)
        {
            var left = GetData(dataObject, Left) ?? "";
            var type = GetDataType(dataObject, Left);
            return Utility.IsNumericType(type)
                       ? CompareNumeric(left, Right, Operation)
                       : Compare(left.ToString(), Right, Operation);
        }

        private bool Compare(string left, string right, string operation)
        {
            switch (operation)
            {
                case Operations.IsNull: return string.IsNullOrWhiteSpace(left);
                case Operations.IsNotNull: return !string.IsNullOrWhiteSpace(left);
                case Operations.Contains: return left.Contains(right);
                case Operations.Starts: return left.StartsWith(right);
                case Operations.Ends: return left.EndsWith(right);
                case Operations.LengthEquals: return left.Length == Convert.ToInt32(right);
                case Operations.Matches: return Regex.IsMatch(left, right);
                case Operations.NotMatches: return !Regex.IsMatch(left, right);
                case Operations.NotEquals: return left != right;
                case Operations.MatchesMod10: return Utility.ValidateCheckDigit(left);
                case Operations.After: return CompareDates(left, right, (l, r) => l > r);
                case Operations.Before: return CompareDates(left, right, (l, r) => l < r);
                case Operations.Greater: return CompareNumeric(left, right, Operations.Greater);
                case Operations.Less: return CompareNumeric(left, right, Operations.Less);
                default: return left == right;
            }
        }

        private static bool CompareDates(string date1, string date2, Func<DateTime, DateTime, bool> comparation)
        {
            DateTime realDate1;
            if (!DateTime.TryParse(date1, out realDate1)) return false;
            DateTime realDate2;
            if (!DateTime.TryParse(date2, out realDate2)) return false;
            return comparation(realDate1, realDate2);
        }

        private bool CompareNumeric(object left, object right, string operation)
        {
            decimal n1;
            decimal.TryParse(left.ToString(), out n1);
            decimal n2;
            decimal.TryParse(right.ToString(), out n2);

            switch (operation)
            {
                case Operations.Greater: return n1 > n2;
                case Operations.Less: return n1 < n2;
                case Operations.NotEquals: return n1 != n2;
                default: return n1 == n2;
            }
        }

        public bool ContainsData(object dataObject, string propertyName)
        {
            return ((IDictionary<string, object>)dataObject).ContainsKey(propertyName);
        }

        public object GetData(object dataObject, string propertyName)
        {
            // Sözlük olup olmadığını kontrol et
            if (dataObject is IDictionary<string, object> dict && dict.ContainsKey(propertyName))
            {
                return dict[propertyName];
            }

            // Reflection kullanarak doğrudan property'yi al
            var propertyInfo = dataObject.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(dataObject);
            }

            return null; // Eğer veri yoksa null döndür
        }
       
        public Type GetDataType(object dataObject, string propertyName)
        {
            // Sözlükse tipini al
            if (dataObject is IDictionary<string, object> dict && dict.ContainsKey(propertyName))
            {
                return dict[propertyName]?.GetType() ?? typeof(string);
            }

            // Reflection kullanarak property'sinin tipini al
            var propertyInfo = dataObject.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            return typeof(string); // Varsayılan tip
        }       
    }
}
