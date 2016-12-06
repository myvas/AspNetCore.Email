using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.EmailMiddleware.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailDto dto);
    }
}
