using Microsoft.AspNetCore.Mvc;

namespace Myvas.AspNetCore.EmailApi.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
