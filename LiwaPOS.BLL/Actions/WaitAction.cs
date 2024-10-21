using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class WaitAction : IAction
    {
        public async Task Execute(string properties)
        {
            var waitProperties = JsonHelper.Deserialize<WaitDTO>(properties);
            if (waitProperties == null)
                return;

            Thread.Sleep(waitProperties.DurationInSecond * 1000);  // Senkron bekleme
        }
    }
}
