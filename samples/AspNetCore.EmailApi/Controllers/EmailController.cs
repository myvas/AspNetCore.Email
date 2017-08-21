using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using AspNetCore.EmailMiddleware.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.WebApi.EmailApi.Controllers
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    [Route("api/v1/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        /// <summary>
        /// Send an Email.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task SendAsync([FromBody]EmailDto input)
        {
            await _emailSender.SendEmailAsync(input);
        }
    }
}