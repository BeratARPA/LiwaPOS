using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class PageOpenedEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData, dynamic dataObject)
        {
            return Task.CompletedTask;
        }
    }
}
