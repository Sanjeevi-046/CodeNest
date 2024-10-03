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

using CodeNest.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeNest.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpcontextAccessor;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
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
