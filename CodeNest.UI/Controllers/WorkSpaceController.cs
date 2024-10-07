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
        private readonly IHttpContextAccessor _contextAccessor;
        public WorkSpaceController(IWorkspaceService workspaceService , IHttpContextAccessor contextAccessor) 
        {
            _workspaceService = workspaceService; 
            _contextAccessor = contextAccessor;
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkspacesDto workspace)
        {
            string user = HttpContext.Session.GetString("UserID");
            bool isCreated = await _workspaceService.CreateWorkspace(workspace, new ObjectId(user));
            if (isCreated)
            {
                string workSpaceId = workspace.Id.ToString();
                _contextAccessor.HttpContext.Session.SetString("WorkspaceID", workSpaceId);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
