using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Services;
using System.Net;
using System.Net.Mail;

namespace LiwaPOS.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(EmailDTO emailDto)
        {
            var smtpClient = new SmtpClient(emailDto.SMTPServer, (int)emailDto.SMTPPort);

            smtpClient.Credentials = new NetworkCredential(emailDto.SMTPUser, emailDto.SMTPPassword);
            smtpClient.EnableSsl = (bool)emailDto.SMTPEnableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailDto.FromEmailAddress),
                Subject = emailDto.Subject,
                Body = emailDto.Message,  // Mesaj gövdesini buraya ekleyebilirsiniz
                IsBodyHtml = true  // Eğer HTML formatında e-posta gönderecekseniz
            };

            if (!string.IsNullOrEmpty(emailDto.ToEmailAddress))
                emailDto.ToEmailAddress.Split(';').ToList().ForEach(x => mailMessage.To.Add(x));

            if (!string.IsNullOrEmpty(emailDto.CCEmailAddresses))
                emailDto.CCEmailAddresses.Split(';').ToList().ForEach(x => mailMessage.CC.Add(x));

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                await LoggingService.LogInfoAsync("Email successfully sent.", typeof(EmailService).Name, emailDto.ToString());
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                await LoggingService.LogErrorAsync($"An error occurred while sending the email: {ex.Message}", typeof(EmailService).Name, emailDto.ToString(), new Exception());
            }
        }
    }
}
