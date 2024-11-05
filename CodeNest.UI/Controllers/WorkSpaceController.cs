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
using CodeNest.UI.Resources;
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
        public IActionResult Create(ObjectId userId)
        {
            return View(new UserWorkspaceFilesDto { UserId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserWorkspaceFilesDto userWorkspace)
        {
            // Check if a workspace with the same name already exists
            WorkspacesDto? existingWorkspace = await _workspaceService
                .GetWorkspaceByName(userWorkspace.UserId.Value, userWorkspace.Workspace.Name);
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
                TempData["Success"] =Resource.CN_Success_1002;
                return RedirectToAction("JsonFormatter", "Formatter", new { userId = result.CreatedBy, workSpaceId = result.Id });
            }

            TempData["Error"] = Resource.CN_Error_1003;
            return RedirectToAction("JsonFormatter", "Formatter", new { userId = userWorkspace.UserId.Value });
        }
    }
}
