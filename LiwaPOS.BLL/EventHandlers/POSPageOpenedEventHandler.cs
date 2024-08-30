using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class POSPageOpenedEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData)
        {
            return Task.CompletedTask;
        }
    }
}
