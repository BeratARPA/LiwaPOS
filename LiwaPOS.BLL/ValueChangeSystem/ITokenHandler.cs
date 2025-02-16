using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.ValueChangeSystem
{
    public interface ITokenHandler
    {
        string Handle(string token, IReadOnlyList<string> args, ValueContext context);
    }
}
