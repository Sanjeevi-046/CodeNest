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
using CodeNest.UI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Diagnostics;

namespace CodeNest.UI.Controllers
{
    public class HomeController : Controller
    {
        public ILogger<HomeController> Logger;
        private readonly IWorkspaceService _workspaceService;
        private readonly IJsonService _jsonService;

        public HomeController(ILogger<HomeController> logger, IWorkspaceService workspaceService, IJsonService jsonService)
        {
            Logger = logger;
            _workspaceService = workspaceService;
            _jsonService = jsonService;
        }

        public async Task<IActionResult> Index(ObjectId userId, ObjectId? workSpaceId)
        {
            List<WorkspacesDto> workspaces = await _workspaceService
                .GetWorkspaces(userId);

            ObjectId workspaceObjectId = workSpaceId == null || workSpaceId == ObjectId.Empty
            ? workspaces[0].Id : workSpaceId.Value;

            List<BlobDto> jsonData = await _jsonService.GetJson(workspaceObjectId);

            UserWorkspaceFilesDto userWorkspace = new()
            {
                UserId = userId,
                WorkspaceId = workspaceObjectId,
                Workspaces = workspaces,
                Blobs = jsonData
            };

            return View(userWorkspace);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}
