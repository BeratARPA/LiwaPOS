using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.Shared.Services;

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

        public async Task ExecuteAppRulesForEventAsync(EventType eventType, dynamic dataObject = null)
        {
            // Event handler'ı tetikleyin
            await HandleEventAsync(eventType, dataObject);

            // İlgili kuralları çalıştırın
            var appRules = await _appRuleService.GetAllAppRulesAsync(r => r.Type == eventType);
            if (appRules != null)
            {
                foreach (var appRule in appRules)
                {
                    await ExecuteActionsForRuleAsync(appRule, dataObject);
                }
            }
        }

        private async Task HandleEventAsync(EventType eventType, dynamic dataObject = null)
        {
            var eventHandler = _eventFactory.GetEventHandler(eventType);
            if (eventHandler != null)
            {
                await eventHandler.HandleEventAsync(eventType.ToString(), dataObject);
            }
            else
            {
                // Hata loglama veya farklı bir hata yönetimi eklenebilir
                await LoggingService.LogErrorAsync($"No event handler found for event type {eventType}.", typeof(AppRuleManager).Name, eventType.ToString(), new InvalidOperationException());
            }
        }

        private async Task ExecuteActionsForRuleAsync(AppRuleDTO appRule, dynamic dataObject = null)
        {
            if (dataObject != null)
            {
                // Kısıtlamaları değerlendir
                bool isConstraintSatisfied = EvaluateRuleConstraints(appRule, dataObject);

                if (!isConstraintSatisfied)
                {
                    await LoggingService.LogInfoAsync($"Rule '{appRule.Name}' constraints not satisfied. Skipping actions.", typeof(AppRuleManager).Name, appRule.Id.ToString());
                    return;
                }
            }

            var rulesActionMaps = await _ruleActionMapService.GetAllRuleActionMapsAsync(r => r.AppRuleId == appRule.Id);
            if (rulesActionMaps != null)
            {
                foreach (var rulesActionMap in rulesActionMaps.OrderBy(x => x.SortOrder))
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
                    await action.Execute(appAction.Properties);
                }
                else
                {
                    // Hata loglama veya farklı bir hata yönetimi eklenebilir
                    await LoggingService.LogErrorAsync($"No action found for action type {appAction.Type}.", typeof(ActionFactory).Name, appAction.Type.ToString(), new InvalidOperationException());
                }
            }
        }

        private bool EvaluateRuleConstraints(AppRuleDTO appRule, object dataObject)
        {
            var constraints = JsonHelper.Deserialize<List<RuleConstraintDTO>>(appRule.Constraints);
            if (constraints == null || !constraints.Any()) return true; // Kısıtlama yoksa, true döndür

            switch (appRule.ConditionMatch)
            {
                case ConditionMatchType.Matches:
                    return constraints.All(c => c.Satisfies(dataObject));
                case ConditionMatchType.MatchesAny:
                    return constraints.Any(c => c.Satisfies(dataObject));
                case ConditionMatchType.MatchesAll:
                    return constraints.All(c => c.Satisfies(dataObject));
                case ConditionMatchType.NotMatchesAny:
                    return !constraints.Any(c => c.Satisfies(dataObject));
                case ConditionMatchType.NotMatchesAll:
                    return !constraints.All(c => c.Satisfies(dataObject));
                default:
                    return false;
            }
        }
    }
}
