using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
