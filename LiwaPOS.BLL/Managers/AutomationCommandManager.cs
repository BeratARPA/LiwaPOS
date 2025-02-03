using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;

namespace LiwaPOS.BLL.Managers
{
    public class AutomationCommandManager
    {
        private readonly AppRuleManager _appRuleManager;
        private readonly INavigatorService _navigatorService;
        private readonly IAutomationCommandService _automationCommandService;
        private readonly IAutomationCommandMapService _automationCommandMapService;

        public AutomationCommandManager(
            AppRuleManager appRuleManager,
            INavigatorService navigatorService,
            IAutomationCommandService automationCommandService,
            IAutomationCommandMapService automationCommandMapService)
        {
            _appRuleManager = appRuleManager;
            _navigatorService = navigatorService;
            _automationCommandService = automationCommandService;
            _automationCommandMapService = automationCommandMapService;
        }

        public async Task<List<AutomationCommandDTO>> GetAutomationCommands(int terminalId, int departmentId, int userRoleId, int ticketTypeId, ScreenVisibilityType displayOn)
        {
            var automationCommandMaps = await _automationCommandMapService.GetAllAutomationCommandMapsAsync(x => x.TerminalId == terminalId && x.DepartmentId == departmentId && x.UserRoleId == userRoleId && x.TicketTypeId == ticketTypeId && x.DisplayOn == displayOn.ToString());

            if (automationCommandMaps.Count() > 0)
            {
                var automationCommands = new List<AutomationCommandDTO>();
                foreach (var automationCommandMap in automationCommandMaps)
                {
                    var automationCommand = await _automationCommandService.GetAutomationCommandByIdAsync((int)automationCommandMap.AutomationCommandId);
                    if (automationCommand != null)
                    {
                        automationCommands.Add(automationCommand);
                    }
                }

                return automationCommands;
            }

            return null;
        }

        public async Task ExecuteCommandAsync(AutomationCommandDTO automationCommand, string selectedValue)
        {
            // Komutun çalıştırılması
            if (!string.IsNullOrEmpty(automationCommand.NavigationModule))
            {
                // İlgili modülü aç
                _navigatorService.Navigate(automationCommand.NavigationModule);
            }

            await _appRuleManager.ExecuteAppRulesForEventAsync(EventType.AutomationCommandExecuted, automationCommand);
        }
    }
}
