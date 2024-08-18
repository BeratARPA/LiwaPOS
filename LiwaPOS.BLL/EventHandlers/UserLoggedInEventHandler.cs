using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class UserLoggedInEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData)
        {
            // User logged in event handling logic
            return Task.CompletedTask;
        }
    }
}
