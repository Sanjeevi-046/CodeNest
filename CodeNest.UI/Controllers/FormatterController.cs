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
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
namespace CodeNest.UI.Controllers
{
    public class FormatterController : Controller
    {
        private readonly IFormatterServices _formatterServices;
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormatterController(IFormatterServices formatterServices , IWorkspaceService workspaceService, IHttpContextAccessor httpContextAccessor)
        {
            _formatterServices = formatterServices;
            _workspaceService = workspaceService;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> SaveJson(JsonDto jsonDto , string? Name  ,string? Description)
        {
            string? userId = HttpContext.Session.GetString("userId");
            string? workspaceId = HttpContext.Session.GetString("workspaceId");
            if(workspaceId == null) 
            {
                WorkspacesDto workspace = new()
                {
                    Name = Name,
                    Description = Description,
                };
                WorkspacesDto result = await _workspaceService.CreateWorkspace(workspace, new ObjectId(userId));
                if (result != null)
                {
                    string workSpaceId = result.Id.ToString();
                    _httpContextAccessor.HttpContext.Session.SetString("workspaceId", workSpaceId);
                    workspaceId = HttpContext.Session.GetString("workspaceId");
                }
            }
            ValidationDto jsonResult = await _formatterServices.Save(jsonDto, new ObjectId(workspaceId), ObjectId.Parse(userId));
            if (jsonResult.IsValid)
            {
                TempData["Success"] = jsonResult.Message;
                return RedirectToAction("Index", "Home");
            }
            return View("JsonFormatter");
        }
    }
}
