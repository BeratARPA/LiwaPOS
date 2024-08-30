using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class WaitAction : IAction
    {
        public void Execute(string properties)
        {
            var waitProperties = JsonHelper.Deserialize<WaitDTO>(properties);
            if (waitProperties == null)
                return;

            Task.Delay(waitProperties.DurationInSecond);
        }
    }
}
