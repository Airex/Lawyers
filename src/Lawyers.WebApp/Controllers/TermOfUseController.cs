using Microsoft.AspNetCore.Mvc;

namespace Lawyers.WebApp.Controllers
{
    public class TermOfUseController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}