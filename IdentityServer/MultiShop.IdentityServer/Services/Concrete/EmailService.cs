using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MultiShop.IdentityServer.Settings;

namespace MultiShop.IdentityServer.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }
        public async Task SendEmail(string to, string link)
        {
            var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Email, _settings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage(_settings.Email, to)
            {
                Subject = "Şifre Sıfırlama",
                Body = $"Şifrenizi sıfırlamak için tıklayın: {link}"
            };

            await client.SendMailAsync(mail);
        }
    }
}
