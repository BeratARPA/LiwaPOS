using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class PopupDisplayedEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData)
        {
            // POS Page opened event handling logic
            return Task.CompletedTask;
        }
    }
}
