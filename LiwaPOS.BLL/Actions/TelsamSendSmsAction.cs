using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class TelsamSendSmsAction : IAction
    {
        private readonly ISmsService _smsService;

        public TelsamSendSmsAction(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public void Execute(string properties)
        {
            var smsProperties = JsonHelper.Deserialize<TelsamSmsDTO>(properties);
            if (smsProperties == null)
                return;

            _smsService.SendSmsAsync(smsProperties);
        }
    }
}
