using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Enums;

namespace LiwaPOS.BLL.Managers
{
    public class AppRuleManager
    {
        private readonly IAppRuleService _appRuleService;
        private readonly ActionFactory _actionFactory;
        private readonly EventFactory _eventFactory;

        public AppRuleManager(IAppRuleService appRuleService, ActionFactory actionFactory, EventFactory eventFactory)
        {
            _appRuleService = appRuleService;
            _actionFactory = actionFactory;
            _eventFactory = eventFactory;
        }

        public async Task ExecuteAppRulesForEventAsync(EventType eventType)
        {
            var eventHandler = _eventFactory.GetEventHandler(eventType);
            if (eventHandler != null)
            {
                await eventHandler.HandleEventAsync(eventType.ToString());
            }
            else
            {
                throw new InvalidOperationException($"No event handler found for event type {eventType}.");
            }

            var appRules = await _appRuleService.GetAllAppRulesAsync(r => r.EventName == eventType.ToString());

            if (appRules != null)
            {
                foreach (var appRule in appRules)
                {
                    if (appRule.Actions != null)
                    {
                        foreach (var appAction in appRule.Actions)
                        {
                            var action = _actionFactory.GetAction(appAction.ActionType);
                            if (action != null)
                            {
                                action.Execute(appAction.Properties);
                            }
                        }
                    }
                }
            }
        }

    }
}
