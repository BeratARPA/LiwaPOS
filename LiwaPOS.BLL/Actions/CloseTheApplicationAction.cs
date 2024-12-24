using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.Actions
{
    public class CloseTheApplicationAction : IAction
    {
        public async Task<object> Execute(string properties)
        {
            Environment.Exit(1);

            return true;
        }
    }
}
