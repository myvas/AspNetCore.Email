using Microsoft.AspNetCore.Mvc;
using System;

namespace Myvas.AspNetCore.EmailApi.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            var dto = new
            {
                Recipient = "noreply@myvas.com",
                Subject = $"Test Email ({DateTime.Now.Ticks})",
                Body = $"This is a test email from WebApi, called by WebClient"
            };

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
