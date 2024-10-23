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
    public class WorkSpaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
       
        public WorkSpaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkSpaces()
        {
            string? user = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(user))
            {
                return Unauthorized();
            }

            List<WorkspacesDto> workspaces = await _workspaceService.GetWorkspaces(new ObjectId(user));
            return Json(new { workspaces });
        }
        [HttpGet]
        public IActionResult Create(ObjectId userId)
        {
            return View(new UserWorkspaceFilesDto { UserId=userId});
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserWorkspaceFilesDto userWorkspace)
        {
            if (!userWorkspace.UserId.HasValue)
            {
                TempData["Error"] = "User ID is required";
                return RedirectToAction("JsonFormatter", "Formatter");
            }

            // Check if a workspace with the same name already exists
            WorkspacesDto? existingWorkspace = await _workspaceService.GetWorkspaceByName(userWorkspace.UserId.Value, userWorkspace.Workspace.Name);
            if (existingWorkspace != null)
            {
                // Map the existing workspace
                return RedirectToAction("JsonFormatter", "Formatter", new { userId = existingWorkspace.CreatedBy, workSpaceId = existingWorkspace.Id });
            }

            // Create a new workspace if it doesn't exist
            WorkspacesDto workspaceDto = new()
            {
                Name = userWorkspace.Workspace.Name,
                CreatedBy = userWorkspace.UserId.Value
            };

            WorkspacesDto result = await _workspaceService.CreateWorkspace(workspaceDto, userWorkspace.UserId.Value);
            if (result != null)
            {
                return RedirectToAction("JsonFormatter", "Formatter", new { userId = result.CreatedBy, workSpaceId = result.Id });
            }

            TempData["Error"] = "Failed to create workspace";
            return RedirectToAction("JsonFormatter", "Formatter", new { userId = userWorkspace.UserId.Value });
        }
    }
}
