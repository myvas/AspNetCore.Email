using System.Threading.Tasks;

namespace AspNetCore.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendEmailAsync(EmailDto input);
    }
}
