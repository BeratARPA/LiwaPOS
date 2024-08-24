using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // JSON verisini ayrıştır
            var smsProperties = JsonHelper.Deserialize<TelsamSmsDTO>(properties);
            if (smsProperties != null)
                _smsService.SendSmsAsync(smsProperties);
        }
    }
}
