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
    public class JsonController : Controller
    {
        private readonly IJsonService _jsonService;
        public JsonController(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }
        public IActionResult Index(JsonDto jsonModel = null)
        {
            return View(jsonModel);
        }
        [HttpGet]
        public async Task<IActionResult> Validate(JsonDto jsonModel = null)
        {
            return View(jsonModel);
        }
        [HttpPost]
        public async Task<IActionResult> Validate(string JsonInput)
        {
            ValidationDto result = await _jsonService.Validate(JsonInput);
            if (result.IsValid)
            {
                ViewBag.Success = result.Message;
                return View(result.JsonDto);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(result.JsonDto);
        }
    }
}
