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
            await eventHandler.HandleEventAsync(eventType.ToString());

            var appRules = await _appRuleService.GetAllAppRulesAsync(r => r.EventName == eventType.ToString());

            foreach (var appRule in appRules)
            {
                foreach (var appAction in appRule.Actions)
                {
                    var action = _actionFactory.GetAction(appAction.ActionType);
                    action.Execute(appAction.Properties);
                }
            }
        }
    }
}
