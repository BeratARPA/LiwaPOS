using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Enums;

namespace LiwaPOS.BLL.Managers
{
    public class AppRuleManager
    {
        private readonly IAppRuleService _appRuleService;
        private readonly IAppActionService _appActionService;
        private readonly IRuleActionMapService _ruleActionMapService;
        private readonly ActionFactory _actionFactory;
        private readonly EventFactory _eventFactory;

        public AppRuleManager(IAppRuleService appRuleService, IAppActionService appActionService, IRuleActionMapService ruleActionMapService, ActionFactory actionFactory, EventFactory eventFactory)
        {
            _appRuleService = appRuleService;
            _appActionService = appActionService;
            _ruleActionMapService = ruleActionMapService;
            _actionFactory = actionFactory;
            _eventFactory = eventFactory;
        }

        public async Task ExecuteAppRulesForEventAsync(EventType eventType)
        {
            // Event handler'ı tetikleyin
            await HandleEventAsync(eventType);

            // İlgili kuralları çalıştırın
            var appRules = await _appRuleService.GetAllAppRulesAsync(r => r.Type == eventType);
            if (appRules != null)
            {
                foreach (var appRule in appRules)
                {
                    await ExecuteActionsForRuleAsync(appRule);
                }
            }
        }

        private async Task HandleEventAsync(EventType eventType)
        {
            var eventHandler = _eventFactory.GetEventHandler(eventType);
            if (eventHandler != null)
            {
                await eventHandler.HandleEventAsync(eventType.ToString());
            }
            else
            {
                // Hata loglama veya farklı bir hata yönetimi eklenebilir
                throw new InvalidOperationException($"No event handler found for event type {eventType}.");
            }
        }

        private async Task ExecuteActionsForRuleAsync(AppRuleDTO appRule)
        {
            var rulesActionMaps = await _ruleActionMapService.GetAllRuleActionMapsAsync(r => r.AppRuleId == appRule.Id);
            if (rulesActionMaps != null)
            {
                foreach (var rulesActionMap in rulesActionMaps)
                {
                    await ExecuteActionForRuleAsync(rulesActionMap);
                }
            }
        }

        private async Task ExecuteActionForRuleAsync(RuleActionMapDTO rulesActionMap)
        {
            var appAction = await _appActionService.GetAppActionByIdAsync(rulesActionMap.AppActionId);
            if (appAction != null)
            {
                var action = _actionFactory.GetAction(appAction.Type);
                if (action != null)
                {
                    action.Execute(appAction.Properties);
                }
                else
                {
                    // Hata loglama veya farklı bir hata yönetimi eklenebilir
                    throw new InvalidOperationException($"No action found for action type {appAction.Type}.");
                }
            }
        }
    }
}
