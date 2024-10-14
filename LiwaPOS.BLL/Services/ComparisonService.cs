using LiwaPOS.Shared.Enums;
using System.Text.RegularExpressions;

namespace LiwaPOS.BLL.Services
{
    public class ComparisonService
    {
        public bool Compare(string left, string right, ComparisonOperation operation)
        {
            switch (operation)
            {
                case ComparisonOperation.IsNull: return string.IsNullOrWhiteSpace(left);
                case ComparisonOperation.IsNotNull: return !string.IsNullOrWhiteSpace(left);
                case ComparisonOperation.Contains: return left.Contains(right);
                case ComparisonOperation.Starts: return left.StartsWith(right);
                case ComparisonOperation.Ends: return left.EndsWith(right);
                case ComparisonOperation.LengthEquals: return left.Length == Convert.ToInt32(right);
                case ComparisonOperation.Matches: return Regex.IsMatch(left, right);
                case ComparisonOperation.NotMatches: return !Regex.IsMatch(left, right);
                case ComparisonOperation.NotEquals: return left != right;
                case ComparisonOperation.MatchesMod10: return UtilityService.ValidateCheckDigit(left);
                case ComparisonOperation.After: return CompareDates(left, right, (l, r) => l > r);
                case ComparisonOperation.Before: return CompareDates(left, right, (l, r) => l < r);
                case ComparisonOperation.Greater: return CompareNumeric(left, right, ComparisonOperation.Greater);
                case ComparisonOperation.Less: return CompareNumeric(left, right, ComparisonOperation.Less);
                default: return left == right;
            }
        }

        private static bool CompareDates(string date1, string date2, Func<DateTime, DateTime, bool> comparison)
        {
            if (DateTime.TryParse(date1, out DateTime realDate1) &&
                DateTime.TryParse(date2, out DateTime realDate2))
            {
                return comparison(realDate1, realDate2);
            }
            return false;
        }

        private bool CompareNumeric(object left, object right, ComparisonOperation operation)
        {
            if (decimal.TryParse(left.ToString(), out decimal n1) &&
                decimal.TryParse(right.ToString(), out decimal n2))
            {
                switch (operation)
                {
                    case ComparisonOperation.Greater: return n1 > n2;
                    case ComparisonOperation.Less: return n1 < n2;
                    case ComparisonOperation.NotEquals: return n1 != n2;
                    default: return n1 == n2;
                }
            }
            return false;
        }

        public bool ContainsData(object dataObject, string propertyName)
        {
            return ((IDictionary<string, object>)dataObject).ContainsKey(propertyName);
        }

        public object GetData(object dataObject, string propertyName)
        {
            if (ContainsData(dataObject, propertyName))
            {
                return ((IDictionary<string, object>)dataObject)[propertyName];
            }
            return propertyName;
        }

        public Type GetDataType(object dataObject, string propertyName)
        {
            if (ContainsData(dataObject, propertyName))
            {
                var value = ((IDictionary<string, object>)dataObject)[propertyName];
                if (value != null)
                    return value.GetType();
            }
            return typeof(string);
        }
    }
}
