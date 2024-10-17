using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class PopupDisplayedEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData, dynamic dataObject)
        {         
            return Task.CompletedTask;
        }
    }
}
