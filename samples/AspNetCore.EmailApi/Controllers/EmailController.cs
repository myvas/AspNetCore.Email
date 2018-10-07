using AspNetCore.Email;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.WebApi.EmailApi.Controllers
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

        /// <summary>
        /// Send an Email.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendAsync([FromBody]EmailDto input)
        {
            var result = await _emailSender.SendEmailAsync(input);
            return Json(result);
        }
    }
}