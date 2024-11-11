// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using CodeNest.DAL.Models;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using MongoDB.Bson;

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
        private async Task GenerateClaimsAsync(string userName)
        {
            ClaimsIdentity identity = new(
                [
                    new (ClaimTypes.Name, userName),
                    new (ClaimTypes.Role, "Users")
                ], CookieAuthenticationDefaults.AuthenticationScheme
            );

            ClaimsPrincipal principal = new(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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
                await GenerateClaimsAsync(result.Name);
                return RedirectToAction("JsonFormatter", "Formatter", new { userId = result.Id });
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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignInWithMicrosoft()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/Auth/MicrosoftUserDetails" }, MicrosoftAccountDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> MicrosoftUserDetails()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                // Get the claims for the authenticated user
                IEnumerable<Claim> claims = result.Principal.Claims;

                // You can extract the user information from the claims
                string? userEmail = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                string? userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                string? userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                UsersDto userDetails = await _userService.GetUserByNameIdentifier(userId);
                if (userDetails != null) 
                {
                    await GenerateClaimsAsync(userDetails.Name);
                    return RedirectToAction("JsonFormatter", "Formatter", new { userId = userDetails.Id });
                }

                UsersDto newUser = new()
                {
                    Name = userName,
                    Email = userEmail,
                    NameIdentifier = userId
                };
                UsersDto registeredUser = await _userService.Register(newUser);
                return RedirectToAction("JsonFormatter", "Formatter", new { userId = registeredUser.Id });
            }

            return Unauthorized();
        }
    }
}
