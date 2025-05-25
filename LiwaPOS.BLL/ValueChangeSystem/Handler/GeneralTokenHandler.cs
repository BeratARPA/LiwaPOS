using Jint.Runtime;
using System.Security.Cryptography;
using System.Text;

namespace LiwaPOS.BLL.ValueChangeSystem.Handler
{
    [Token("CALL", "DATE", "TIME", "RANDOM", "RANDOMC", "GUID", "SHA256")]
    public sealed class GeneralTokenHandler : ITokenHandler
    {
        private static readonly Random _random = new Random();

        public string Handle(string token, IReadOnlyList<string> args, ValueContext context)
        {
            return token switch
            {
                "CALL" => HandleCallToken(args, context),
                "DATE" => HandleDateToken(args),
                "TIME" => HandleTimeToken(args),
                "RANDOM" => HandleRandomToken(args),
                "RANDOMC" => HandleRandomWithCheckDigitToken(args),
                "GUID" => HandleGuidToken(args),
                "SHA256" => HandleSHA256Token(args),
                _ => string.Empty
            };
        }

        private string HandleGuidToken(IReadOnlyList<string> args)
        {
            return Guid.NewGuid().ToString();
        }

        private string HandleSHA256Token(IReadOnlyList<string> args)
        {
            if (args == null || args.Count == 0)
                return string.Empty;

            string input = args[0];
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string HandleCallToken(IReadOnlyList<string> args, ValueContext context)
        {
            if (context.JavaScriptEngine == null || args.Count == 0)
                return string.Empty;

            var script = string.Join(" ", args);
            try
            {
                var result = context.JavaScriptEngine.Evaluate(script).ToObject();
                return result?.ToString() ?? string.Empty;
            }
            catch (JavaScriptException ex)
            {
                // Hata yönetimi
                return $"[ERROR: {ex.Message}]";
            }
        }

        private string HandleDateToken(IReadOnlyList<string> args)
        {
            var format = args.Count > 0 ? args[0] : "d";
            return DateTime.Now.ToString(format);
        }

        private string HandleTimeToken(IReadOnlyList<string> args)
        {
            var format = args.Count > 0 ? args[0] : "t";
            return DateTime.Now.ToString(format);
        }

        private string HandleRandomToken(IReadOnlyList<string> args)
        {
            if (args.Count == 0)
                return string.Empty;

            // İlk argüman uzunluk bilgisidir
            if (!int.TryParse(args[0], out var length) || length <= 0)
                return string.Empty;

            // İkinci argüman varsa, allowed karakter seti; yoksa varsayılan alfanümerik küme kullanılır
            string allowedChars = args.Count >= 2 && !string.IsNullOrEmpty(args[1])
                ? args[1]
                : "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(allowedChars.Length);
                result.Append(allowedChars[index]);
            }

            return result.ToString();
        }

        private string HandleRandomWithCheckDigitToken(IReadOnlyList<string> args)
        {
            if (args.Count == 0)
                return string.Empty;

            if (!int.TryParse(args[0], out var length) || length <= 0)
                return string.Empty;

            // {RANDOMC:X} için numeric (0123456789) karakter seti kullanılır.
            const string allowedChars = "0123456789";
            var randomNumber = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(allowedChars.Length);
                randomNumber.Append(allowedChars[index]);
            }

            // Check digit hesaplaması (Modulo10 algoritması)
            int checkDigit = CalculateCheckDigit(randomNumber.ToString());
            return randomNumber.ToString() + checkDigit.ToString();
        }

        private int CalculateCheckDigit(string number)
        {
            // Basit Luhn algoritması benzeri hesaplama
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                int digit = int.Parse(number[i].ToString());
                // İndeks çiftse çarpma işlemi uygulanıyor
                if (i % 2 == 0)
                    digit *= 2;
                sum += digit > 9 ? digit - 9 : digit;
            }
            return (10 - (sum % 10)) % 10;
        }
    }
}
