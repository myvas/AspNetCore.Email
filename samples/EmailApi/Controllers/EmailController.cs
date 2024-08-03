using Microsoft.AspNetCore.Mvc;
using Myvas.AspNetCore.Email;
using System;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.WebApi.EmailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailSender emailSender, ILogger<EmailController> logger)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _logger = logger;
        }

        [HttpGet("samples/plain")]
        public EmailDto PlainEmailSample()
        {
            var result = new EmailDto()
            {
                Recipients = "4848285@qq.com;noreply@myvas.com",
                Subject = "Test Myvas.AspNetCore.Email (Plain Text)",
                Body = @"Hello, 

This is a TEST email from WebApi.

Best Regards,
Myvas.AspNetCore.Email",
            };

            return result;
        }

        [HttpGet("samples/html")]
        public EmailDto HtmlEmailSample()
        {
            var result = new EmailDto()
            {
                Recipients = "4848285@qq.com;noreply@myvas.com",
                Subject = "Test Myvas.AspNetCore.Email (HTML)",
                Body = @"<h1>Hello, </h1>
<p>This is a <strong>TEST</strong> email from WebApi.</p>
<br />
<p>Best Regards,</p>
<p>Myvas.AspNetCore.Email</p>",
                IsBodyHtml = true
            };

            return result;
        }


        /// <summary>
        /// Send an Email.
        /// </summary>
        /// <remarks>
        /// {
        ///  "recipients": "4848285@qq.com;noreply@test.com",
        ///  "subject": "Test Myvas.AspNetCore.Email", 
        ///  "body": "This is a test email from Myvas.AspNetCore.Email/WebApi."
        /// }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SendAsync([FromBody]EmailDto input)
        {
            var result = await _emailSender.SendEmailAsync(input);
            return result;
        }
    }
}