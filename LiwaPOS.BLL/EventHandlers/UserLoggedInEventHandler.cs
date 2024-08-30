using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.EventHandlers
{
    public class UserLoggedInEventHandler : IEventHandler
    {
        public Task HandleEventAsync(string eventData)
        {          
            return Task.CompletedTask;
        }
    }
}
