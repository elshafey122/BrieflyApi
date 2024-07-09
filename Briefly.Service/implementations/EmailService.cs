using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Briefly.Service.implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        public EmailService(EmailSetting emailSetting)
        {
            _emailSetting = emailSetting;
        }

        public async Task<string> SendEmailAsync(string email, string message, string subject)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSetting.Host, _emailSetting.Port, SecureSocketOptions.Auto);
                    client.Authenticate(_emailSetting.FromEmail, _emailSetting.Password);

                    var bodyBuilder = new BodyBuilder()
                    {
                        TextBody = "Welcome",
                        HtmlBody = $"{message}"
                    };
                    var Message = new MimeMessage()
                    {
                        Body = bodyBuilder.ToMessageBody(),
                    };
                    Message.From.Add(new MailboxAddress("NewsApp", _emailSetting.FromEmail));
                    Message.To.Add(new MailboxAddress("customers", email));
                    Message.Subject = subject == null ? "noSubtitled" : subject;
                    await client.SendAsync(Message);
                    await client.DisconnectAsync(true);

                    client.Dispose();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}
