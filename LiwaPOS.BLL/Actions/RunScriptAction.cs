using LiwaPOS.BLL.Interfaces;
using LiwaPOS.BLL.Services;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Services;

namespace LiwaPOS.BLL.Actions
{
    public class RunScriptAction : IAction
    {
        private readonly IScriptService _scriptService;
        private readonly JavaScriptEngineService _javaScriptEngineService;

        public RunScriptAction(IScriptService scriptService, JavaScriptEngineService javaScriptEngineService)
        {
            _scriptService = scriptService;
            _javaScriptEngineService = javaScriptEngineService;
        }

        public async Task Execute(string properties)
        {
            var scriptProperties = JsonHelper.Deserialize<RunScriptDTO>(properties);
            if (scriptProperties == null)
                return;

            if (string.IsNullOrEmpty(scriptProperties.Name))
            {
                await LoggingService.LogErrorAsync("Script name is null or empty.", typeof(RunScriptAction).Name, scriptProperties.ToString(), new ArgumentNullException());
                return;
            }

            // Veritabanından scripti al
            var script = await _scriptService.GetScriptAsNoTrackingAsync(x => x.Name == scriptProperties.Name);
            if (script == null)
            {            
                await LoggingService.LogErrorAsync($"No script found with name {scriptProperties.Name}.", typeof(RunScriptAction).Name, script.ToString(), new ArgumentNullException());
                return;
            }

            // Script'i çalıştır
            _javaScriptEngineService.ExecuteJavaScript(script.Code);
        }
    }
}
