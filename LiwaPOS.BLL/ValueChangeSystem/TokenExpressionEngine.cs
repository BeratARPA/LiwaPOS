using LiwaPOS.BLL.Services;
using System.Text.RegularExpressions;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public sealed class TokenExpressionEngine : IDynamicValueResolver
    {
        private static readonly Regex _tokenPattern = new Regex(
            @"\[(?<token>[A-Z0-9_ :]+)(?:\|(?<args>.*?))?\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly TokenRegistry _registry;
        private readonly JavaScriptEngineService _javaScriptEngineService;
  
        public TokenExpressionEngine(TokenRegistry registry, JavaScriptEngineService javaScriptEngineService)
        {
            _registry = registry;
            _javaScriptEngineService = javaScriptEngineService;
        }

        public string ResolveExpression(string expression, ValueContext context)
        {
            // CALL token'ı varsa yeni motor oluştur
            if (_tokenPattern.IsMatch(expression) && expression.Contains("[CALL"))
                context.JavaScriptEngine = _javaScriptEngineService.CreateNewEngine();

            return _tokenPattern.Replace(expression, match =>
            {
                var token = match.Groups["token"].Value.Trim();
                var args = ParseArguments(match.Groups["args"].Value);

                return _registry.ResolveToken(token, args, context) ?? match.Value;
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
