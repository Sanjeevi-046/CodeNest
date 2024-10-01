using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using CodeNest.DAL.Models;


namespace CodeNest.UI.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthController(IUserService userService , IHttpContextAccessor httpContextAccessor)
    {
        _userService= userService;
        _httpContextAccessor= httpContextAccessor;
    }
    public IActionResult ForgotPasswordBasic() => View();
    public IActionResult LoginBasic() => View();
    public IActionResult RegisterBasic() => View();
    /// <summary>
    /// User Login Validation
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> LoginBasic(UsersDto user)
    {
        var result = await _userService.Login(user.Name, user.Password);
        if (result != null)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserID", result.Id);
            _httpContextAccessor.HttpContext.Session.SetString("userName", result.Name);

            return RedirectToAction("Index", "Home");
        }
        return View();
    }

}
