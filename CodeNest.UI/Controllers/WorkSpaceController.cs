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
    public class WorkSpaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _contextAccessor;
        public WorkSpaceController(IWorkspaceService workspaceService, IHttpContextAccessor contextAccessor)
        {
            _workspaceService = workspaceService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> WorkSpaces()
        {
            string? user = HttpContext.Session.GetString("userId");
             
            List<WorkspacesDto> workspaces = await _workspaceService.GetWorkspaces(new ObjectId(user));
            return Json(new 
            {
                workspaces 
            });    
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkspacesDto workspace)
        {
            string? user = HttpContext.Session.GetString("userId");
            WorkspacesDto result = await _workspaceService.CreateWorkspace(workspace, new ObjectId(user));
            if (result!=null)
            {
                string workSpaceId = result.Id.ToString();
                _contextAccessor.HttpContext.Session.SetString("workspaceId", workSpaceId);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
