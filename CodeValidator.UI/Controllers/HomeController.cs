using CodeValidator.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(WorkspacesDto workspacesDto)
        {
            return View();
        }
    }
}
