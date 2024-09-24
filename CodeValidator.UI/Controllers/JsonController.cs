using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
