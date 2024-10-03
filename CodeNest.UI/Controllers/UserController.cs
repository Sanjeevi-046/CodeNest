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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsersDto user)
        {
            UsersDto result = await _userService.Login(user.Name, user.Password);
            if (result != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("UserID", result.Id);
                _httpContextAccessor.HttpContext.Session.SetString("userName", result.Name);

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
                UsersDto result = await _userService.Register(registeredUser);

                if (result!=null)
                {
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
