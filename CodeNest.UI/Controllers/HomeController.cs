using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.UI.Controllers
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
