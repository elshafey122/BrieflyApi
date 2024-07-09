
namespace Briefly.Service.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string email, string message, string subject);
    }
}
