using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models;
using System.Net;
using System.Net.Mail;

namespace LiwaPOS.BLL.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(EmailDTO emailDto)
        {
            using (var smtpClient = new SmtpClient(emailDto.SMTPServer, (int)emailDto.SMTPPort)
            {
                Credentials = new NetworkCredential(emailDto.SMTPUser, emailDto.SMTPPassword),
                EnableSsl = (bool)emailDto.SMTPEnableSsl
            })
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailDto.FromEmailAddress),
                    Subject = emailDto.Subject,
                    Body = emailDto.Message,  // Mesaj gövdesini buraya ekleyebilirsiniz
                    IsBodyHtml = true  // Eğer HTML formatında e-posta gönderecekseniz
                };

                mailMessage.To.Add(emailDto.ToEmailAddress);

                if (!string.IsNullOrEmpty(emailDto.CCEmailAddresses))
                {
                    foreach (var cc in emailDto.CCEmailAddresses.Split(';'))
                    {
                        mailMessage.CC.Add(cc);
                    }
                }

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Hata yönetimi
                    throw new Exception($"An error occurred while sending the email: {ex.Message} ");
                }
            }
        }
    }
}
