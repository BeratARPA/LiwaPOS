using LiwaPOS.BLL.EventHandlers;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Services;

namespace LiwaPOS.BLL.Factories
{
    public class EventFactory
    {
        private readonly Dictionary<EventType, Type> _eventHandlers = new Dictionary<EventType, Type>
        {
            { EventType.PageOpened, typeof(PageOpenedEventHandler) },
            { EventType.UserLoggedIn, typeof(UserLoggedInEventHandler) },
            { EventType.PopupDisplayed, typeof(PopupDisplayedEventHandler) },
            { EventType.UserFailedToLogin, typeof(UserFailedToLoginEventHandler) },
            { EventType.ShellInitialized, typeof(ShellInitializedEventHandler) },
            { EventType.AutomationCommandExecuted, typeof(AutomationCommandExecutedEventHandler) },
            { EventType.PopupClicked, typeof(PopupClickedEventHandler) },
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
                var handler = _serviceProvider.GetService(handlerType) as IEventHandler;
                if (handler != null)
                {
                    return handler;
                }
                else
                {
                    LoggingService.LogErrorAsync($"No service found for event handler type {handlerType.Name}.", typeof(EventFactory).Name, eventType.ToString(), new InvalidOperationException());
                }
            }

            LoggingService.LogErrorAsync($"Event type {eventType} is not implemented.", typeof(EventFactory).Name, eventType.ToString(), new NotImplementedException());

            return null;
        }
    }
}
