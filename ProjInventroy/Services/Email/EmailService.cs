using System.Net;
using System.Net.Mail;

namespace ProjInventroy.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        
        public EmailService(IConfiguration config)
        {
            this._config = config;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new Exception("Receiver email is missing.");
            var smtpSettings = _config.GetSection("Smtp");
            if (string.IsNullOrWhiteSpace(smtpSettings["From"]))
                throw new Exception("Sender email (From) is missing in appsettings.");
            var message = new MailMessage
            {
                From = new MailAddress(smtpSettings["From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(to);

            var smtpClient = new SmtpClient
            {
                Host = smtpSettings["Host"],
                Port = int.Parse(smtpSettings["Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    smtpSettings["Username"],
                    smtpSettings["Password"]
                    )
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
