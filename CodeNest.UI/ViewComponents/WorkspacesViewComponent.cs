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

namespace CodeNest.UI.ViewComponents
{
    [ViewComponent(Name = "Workspaces")]
    public class WorkspacesViewComponent : ViewComponent
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspacesViewComponent(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? user = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(user))
            {
                return Content(string.Empty); 
            }

            List<WorkspacesDto> workspaces = await _workspaceService.GetWorkspaces(new ObjectId(user));
            return View(workspaces);
        }
    }
}
