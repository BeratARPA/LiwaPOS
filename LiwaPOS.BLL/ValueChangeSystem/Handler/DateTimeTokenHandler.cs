using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.ValueChangeSystem.Handler
{
    [Token("DATE", "TIME", "TIMESTAMP")]
    public sealed class DateTimeTokenHandler : ITokenHandler
    {
        public string Handle(string token, IReadOnlyList<string> args, ValueContext context)
        {
            return token switch
            {
                "DATE" => context.Timestamp.ToString(args.FirstOrDefault() ?? "d"),
                "TIME" => context.Timestamp.ToString(args.FirstOrDefault() ?? "t"),
                "TIMESTAMP" => context.Timestamp.ToString("O"),
                _ => string.Empty
            };
        }
    }
}
