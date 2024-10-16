// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: Sample Description.
//
// ***********************************************************************************************

using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult ForgotPasswordBasic() => View();
        public IActionResult Login() => View();
        public IActionResult Register() => View();
        /// <summary>
        /// User Login Validation
        /// </summary>
        /// <param name="user"></param>
        /// <returns>the user detail if exist </returns>
        [HttpPost]
        public async Task<IActionResult> Login(UsersDto user)
        {
            UsersDto result = await _userService.Login(user.Name, user.Password);
            if (result != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("userId", result.Id.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("userName", result.Name);
                return RedirectToAction("Index", "Home", new { userId = result.Id });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsersDto user)
        {
            if (ModelState.IsValid)
            {
                UsersDto result = await _userService.Register(user);
                if (result != null)
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("userId", result.Id.ToString());
                    _httpContextAccessor.HttpContext?.Session.SetString("userName", result.Name);
                    return RedirectToAction("Login");
                }
            }

            return View(user);
        }

        /// <summary>
        /// User Logout
        /// </summary>
        /// <returns>Redirects to Login page</returns>
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
