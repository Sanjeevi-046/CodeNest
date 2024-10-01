using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace CodeNest.UI.Controllers;

public class CardsController : Controller
{
  public IActionResult Basic() => View();
}
