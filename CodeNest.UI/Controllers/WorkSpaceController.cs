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
    [Route("api/[controller]")]
    public class WorkSpaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IHttpContextAccessor _contextAccessor;

        public WorkSpaceController(IWorkspaceService workspaceService, IHttpContextAccessor contextAccessor)
        {
            _workspaceService = workspaceService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("GetWorkSpaces")]
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

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] WorkspacesDto workspace)
        {
            string? user = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(user))
            {
                return Unauthorized();
            }

            WorkspacesDto result = await _workspaceService.CreateWorkspace(workspace, new ObjectId(user));
            if (result != null)
            {
                string workSpaceId = result.Id.ToString();
                _contextAccessor.HttpContext.Session.SetString("workspaceId", workSpaceId);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
