using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsersDto user)
        {
            var result = await _userService.Login(user.Name, user.Password);
            if (result != null)
            {
                HttpContext.Session.SetString("UserID", result.Id);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsersDto registeredUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(registeredUser);

                if (result)
                {
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
