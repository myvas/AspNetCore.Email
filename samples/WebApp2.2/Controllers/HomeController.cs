using Microsoft.AspNetCore.Mvc;
using Myvas.AspNetCore.Email;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _email;
        private readonly IEmailTemplate _template;

        public HomeController(IEmailSender email, IEmailTemplate template)
        {
            _email = email;
            _template = template;
        }

        public IActionResult Index() => View();
        public IActionResult SendEmail() => View();
        public IActionResult SendNamedTemplatedEmail() => View();
        public IActionResult SendOrderedTemplatedEmail() => View();

        [HttpPost, ActionName(nameof(SendEmail))]
        public async Task<IActionResult> SendEmailPost(EmailViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _email.SendEmailAsync(vm.Recipient, vm.Subject, vm.Body, vm.IsHtml);
            var resultDesc = result ? "success" : "failed";
            ViewData["Result"] = $"{DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]")} {resultDesc}";
            return View();
        }

        [HttpPost, ActionName(nameof(SendNamedTemplatedEmail))]
        public async Task<IActionResult> SendNamedTemplatedEmailPost(TemplatedEmailViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var parameters = new Dictionary<string, string>();
            parameters.Add("[[[user]]]", vm.RecipientName);
            parameters.Add("[[[date]]]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            var body = _template.GetContent(vm.Template, parameters);

            var result = await _email.SendEmailAsync(vm.Recipient, vm.Subject, body, vm.IsHtml);
            var resultDesc = result ? "success" : "failed";
            ViewData["Result"] = $"{DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]")} {resultDesc}";

            return View();
        }


        [HttpPost, ActionName(nameof(SendOrderedTemplatedEmail))]
        public async Task<IActionResult> SendOrderedTemplatedEmailPost(TemplatedEmailViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var body = _template.GetContent(vm.Template, vm.RecipientName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            var result = await _email.SendEmailAsync(vm.Recipient, vm.Subject, body, vm.IsHtml);
            var resultDesc = result ? "success" : "failed";
            ViewData["Result"] = $"{DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]")} {resultDesc}";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
