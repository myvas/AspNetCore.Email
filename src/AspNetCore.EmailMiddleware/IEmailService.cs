using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.EmailMiddleware.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendEmailAsync(EmailDto input);
    }
}
