using System.Diagnostics;
using CodeNest.UI.Models;
using Microsoft.AspNetCore.Mvc;


namespace CodeNest.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpcontextAccessor;

    public HomeController(ILogger<HomeController> logger , IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _httpcontextAccessor = contextAccessor;
    }

    public IActionResult Index()
    {
        _httpcontextAccessor.HttpContext = _httpcontextAccessor.HttpContext;
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
     
        
        
       

    }
}
