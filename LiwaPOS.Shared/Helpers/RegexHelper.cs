using System.Text.RegularExpressions;

namespace LiwaPOS.Shared.Helpers
{
    public static class RegexHelper
    {
        // E-mail doğrulama regex
        private const string EmailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
  
        // URL doğrulama regex
        private const string UrlPattern = @"^(http|https):\/\/[^\s/$.?#].[^\s]*$";

        /// <summary>
        /// Generic bir doğrulama fonksiyonu. Verilen desene göre herhangi bir tipi doğrular.
        /// </summary>
        public static bool IsValid<T>(T input, string pattern)
        {
            if (input == null) return false;
            string inputAsString = input.ToString() ?? string.Empty;
            return Regex.IsMatch(inputAsString, pattern);
        }

        /// <summary>
        /// Generic bir doğrulama fonksiyonu, dinamik bir doğrulama işlemi uygular.
        /// </summary>
        public static bool IsValid<T>(T input, Func<string, bool> customValidation)
        {
            if (input == null) return false;
            string inputAsString = input.ToString() ?? string.Empty;
            return customValidation(inputAsString);
        }

        /// <summary>
        /// E-posta doğrulama.
        /// </summary>
        public static bool IsValidEmail<T>(T input)
        {
            return IsValid(input, EmailPattern);
        }

        /// <summary>
        /// URL doğrulama.
        /// </summary>
        public static bool IsValidUrl<T>(T input)
        {
            return IsValid(input, UrlPattern);
        }

        /// <summary>
        /// Dinamik desene göre ilk eşleşen değeri bulur.
        /// </summary>
        public static string? FindFirstMatch<T>(T input, string pattern)
        {
            if (input == null) return null;
            string inputAsString = input.ToString() ?? string.Empty;
            var match = Regex.Match(inputAsString, pattern);
            return match.Success ? match.Value : null;
        }

        /// <summary>
        /// Dinamik desene göre tüm eşleşmeleri bulur.
        /// </summary>
        public static List<string> FindAllMatches<T>(T input, string pattern)
        {
            if (input == null) return new List<string>();
            string inputAsString = input.ToString() ?? string.Empty;
            var matches = Regex.Matches(inputAsString, pattern);
            var results = new List<string>();
            foreach (Match match in matches)
            {
                results.Add(match.Groups[1].Value);
            }
            return results;
        }

        /// <summary>
        /// Dinamik desene göre eşleşen kısımları değiştirir.
        /// </summary>
        public static string ReplaceMatches<T>(T input, string pattern, string replacement)
        {
            if (input == null) return string.Empty;
            string inputAsString = input.ToString() ?? string.Empty;
            return Regex.Replace(inputAsString, pattern, replacement);
        }

        /// <summary>
        /// Dinamik desene göre metni böler.
        /// </summary>
        public static string[] SplitByPattern<T>(T input, string pattern)
        {
            if (input == null) return Array.Empty<string>();
            string inputAsString = input.ToString() ?? string.Empty;
            return Regex.Split(inputAsString, pattern);
        }
    }
}
