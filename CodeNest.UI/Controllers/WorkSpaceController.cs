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
            WorkspacesDto result = await _workspaceService.CreateWorkspace(userWorkspace.Workspace, userWorkspace.UserId.Value);
            if (result != null)
            {

                return RedirectToAction("JsonFormatter", "Formatter", new { userId = result.CreatedBy, workSpaceId = result.Id });
            }
            TempData["Error"] = "Already have workspace in same name";
            return RedirectToAction("JsonFormatter", "Formatter", new { userId = userWorkspace.UserId.Value});
        }
    }
}
