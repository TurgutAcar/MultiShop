using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to, string link);
    }
}
