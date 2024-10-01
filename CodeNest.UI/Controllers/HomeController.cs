using System.Diagnostics;
using CodeNest.UI.Models;
using Microsoft.AspNetCore.Mvc;


namespace CodeNest.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
