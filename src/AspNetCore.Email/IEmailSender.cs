using System.Threading.Tasks;

namespace AspNetCore.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string recipient, string subject, string body);
        Task<bool> SendEmailAsync(EmailDto input);
    }
}
