using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO emailDto);
    }
}
