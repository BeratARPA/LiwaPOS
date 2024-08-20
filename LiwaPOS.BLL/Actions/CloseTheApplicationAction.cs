using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.Actions
{
    public class CloseTheApplicationAction : IAction
    {
        public void Execute(string properties)
        {
           Environment.Exit(1);
        }
    }
}
