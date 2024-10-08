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
using MongoDB.Bson;
namespace CodeNest.UI.Controllers
{
    public class FormatterController : Controller
    {
        private readonly IFormatterServices _formatterServices;

        public FormatterController(IFormatterServices formatterServices)
        {
            _formatterServices = formatterServices;
        }
        public async Task<IActionResult> JsonFormatter()
        {
            // Simulate an asynchronous operation to avoid CS1998 warning
            await Task.CompletedTask;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> JsonFormatter(JsonDto? jsonDto)
        {
            ValidationDto result = await _formatterServices.JsonValidate(jsonDto);
            jsonDto.JsonOutput = result.JsonDto?.JsonOutput ?? string.Empty;
            JsonDto json = result.JsonDto;
            if (result.IsValid)
            {
                TempData["Success"] = result.Message;
                return View(json);
            }

            TempData["Error"] = result.Message;
            return View(jsonDto);
        }
       
        [HttpPost]
        public async Task<IActionResult> SaveJson(JsonDto jsonDto)
        {
            string? userId = HttpContext.Session.GetString("userId");
            string? workspaceId = HttpContext.Session.GetString("workspaceId");
            ValidationDto result = await _formatterServices.Save(jsonDto, new ObjectId(workspaceId), ObjectId.Parse(userId));
            if (result.IsValid)
            {
                TempData["Success"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            return View("JsonFormatter");
        }
    }
}
