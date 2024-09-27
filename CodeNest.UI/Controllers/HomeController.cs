using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public HomeController(IHttpContextAccessor contextAccessor)
        {
            _httpcontextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            _httpcontextAccessor.HttpContext.Session.GetString("userName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(WorkspacesDto workspacesDto)
        {
            return View();
        }
    }
}
