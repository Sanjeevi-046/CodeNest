using CodeValidator.BLL.Service;
using CodeValidator.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
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
        public async Task<IActionResult> Login(UserDto user)
        {
            var result = await _userService.Login(user.Name, user.Password);
            if (result)
            {
                return RedirectToAction("");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto registeredUser)
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
