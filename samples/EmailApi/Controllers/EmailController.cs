using Microsoft.AspNetCore.Mvc;
using Myvas.AspNetCore.Email;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Myvas.AspNetCore.WebApi.EmailApi.Controllers
{
    /// <summary>
    /// Email
    /// </summary>
    [Route("api/v1/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [HttpGet("plain-sample")]
        public IActionResult PlainEmailSample()
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

            return Json(result);
        }

        [HttpGet("html-sample")]
        public IActionResult HtmlEmailSample()
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

            return Json(result);
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
        public async Task<IActionResult> SendAsync([FromBody]EmailDto input)
        {
            var result = await _emailSender.SendEmailAsync(input);
            return Json(result);
        }
    }
}