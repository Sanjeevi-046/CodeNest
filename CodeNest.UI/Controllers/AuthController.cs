using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace CodeNest.UI.Controllers;

public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();

}
