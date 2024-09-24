using CodeValidator.BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetString("UserID");
            var result = await _userService.GetUserById(userId);
            if (result != null) 
            { 
                return View(result);   
            }
            return View();
        }
    }
}
