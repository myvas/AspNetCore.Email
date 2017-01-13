using AspNetCore.EmailMiddleware.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.EmailMiddleware.Services
{
    public class EmailService : IEmailSender
    {
        protected EmailManager _emailManager;
        public EmailService(IOptions<EmailOptions> optionsAccessor)
        {
            _emailManager = new EmailManager(optionsAccessor.Value);
        }

        public async Task SendEmailAsync(string recipients, string subject, string body)
        {
            await _emailManager.SendAsync(recipients, subject, body);
        }

        public async Task SendEmailAsync(EmailDto input)
        {
            await _emailManager.SendAsync(input.Recipients, input.Subject, input.Body);
        }
    }
}
