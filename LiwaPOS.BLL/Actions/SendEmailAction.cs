using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class SendEmailAction : IAction
    {
        private readonly IEmailService _emailService;

        public SendEmailAction(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Execute(string properties)
        {
            var emailProperties = JsonHelper.Deserialize<EmailDTO>(properties);
            if (emailProperties == null)
                return;

            _emailService.SendEmailAsync(emailProperties);
        }
    }
}
