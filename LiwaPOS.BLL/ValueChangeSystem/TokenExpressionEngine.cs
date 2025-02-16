using LiwaPOS.Shared.Models;
using System.Text.RegularExpressions;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public sealed class TokenExpressionEngine : IDynamicValueResolver
    {
        private static readonly Regex _tokenPattern = new Regex(
            @"\[(?<token>[A-Z0-9_ :]+)(?:\|(?<args>.*?))?\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly TokenRegistry _registry;

        public TokenExpressionEngine(TokenRegistry registry) => _registry = registry;

        public string ResolveExpression(string expression, ValueContext context)
        {
            return _tokenPattern.Replace(expression, match =>
            {
                var token = match.Groups["token"].Value.Trim();
                var args = ParseArguments(match.Groups["args"].Value);

                // Context kopyasını oluştur
                var localContext = context;
                return _registry.ResolveToken(token, args, localContext) ?? match.Value;
            });
        }
        private IReadOnlyList<string> ParseArguments(string input)
        {
            if (string.IsNullOrEmpty(input))
                return Array.Empty<string>();

            return input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(arg => arg.Trim())
                        .ToList();
        }
    }
}
