using LiwaPOS.BLL.EventHandlers;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Enums;

namespace LiwaPOS.BLL.Factories
{
    public class EventFactory
    {
        private readonly Dictionary<EventType, Type> _eventHandlers = new Dictionary<EventType, Type>
        {
            { EventType.POSPageOpened, typeof(POSPageOpenedEventHandler) },
            { EventType.UserLoggedIn, typeof(UserLoggedInEventHandler) },
            { EventType.PopupDisplayed, typeof(PopupDisplayedEventHandler) },
            { EventType.UserFailedToLogin, typeof(UserFailedToLoginEventHandler) },
        };

        private readonly IServiceProvider _serviceProvider;

        public EventFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEventHandler GetEventHandler(EventType eventType)
        {
            if (_eventHandlers.TryGetValue(eventType, out var handlerType))
            {
                return _serviceProvider.GetService(handlerType) as IEventHandler;
            }

            throw new NotImplementedException($"Event type {eventType} is not implemented.");
        }
    }
}
