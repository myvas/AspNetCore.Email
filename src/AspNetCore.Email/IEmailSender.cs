using System.Threading.Tasks;

namespace Myvas.AspNetCore.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string recipient, string subject, string htmlBody);
        Task<bool> SendEmailPlainAsync(string recipient, string subject, string plainBody);
        Task<bool> SendEmailHtmlAsync(string recipient, string subject, string htmlBody);
        Task<bool> SendEmailAsync(string recipients, string subject, string plainBody, bool isBodyHtml);
        Task<bool> SendEmailAsync(EmailDto input);
    }
}
