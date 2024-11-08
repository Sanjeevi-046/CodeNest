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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CodeNest.UI.Controllers
{
    [Authorize]
    public class FormatterController : Controller
    {
        private readonly IFormatterServices _formatterServices;
        private readonly IWorkspaceService _workspaceService;
        private readonly IJsonService _jsonService;

        public FormatterController(IFormatterServices formatterServices, IWorkspaceService workspaceService, IJsonService jsonService)
        {
            _formatterServices = formatterServices;
            _workspaceService = workspaceService;
            _jsonService = jsonService;
        }

        private async Task<UserWorkspaceFilesDto> GetUserWorkSpaceDetails(ObjectId? workSpaceId, ObjectId userId, ObjectId? blobId = null)
        {
            List<WorkspacesDto> workspaces = [];
            ObjectId workspaceObjectId = ObjectId.Empty;
            WorkspacesDto? workspace = null;
            List<BlobDto> blobsList = [];
            BlobDto? blob = null;
            // Fetch workspaces for the given userId
            workspaces = await _workspaceService.GetWorkspaces(userId);

            // Determine the workspace to use
            if (workSpaceId != null && workSpaceId != ObjectId.Empty)
            {
                workspaceObjectId = workSpaceId.Value;
            }
            else if (workspaces.Count != 0)
            {
                workspace = workspaces.FirstOrDefault();
                workspaceObjectId = workspace?.Id ?? ObjectId.Empty;
            }

            if (workspaceObjectId != ObjectId.Empty)
            {
                workspace = workspaces.FirstOrDefault(w => w.Id == workspaceObjectId);
                blobsList = await _jsonService.GetJson(workspaceObjectId);
            }

            if (blobId != null)
            {
                blob = await _formatterServices.GetBlob(blobId.Value);
            }

            UserWorkspaceFilesDto userWorkspace = new()
            {
                UserId = userId,
                BlobId = blobId,
                WorkspaceName = workspace?.Name,
                WorkspaceId = workspaceObjectId,
                Workspaces = workspaces,
                BlobsList = blobsList,
                Blob = blob
            };

            return userWorkspace;
        }

        public async Task<IActionResult> JsonFormatter(ObjectId userId, ObjectId? workSpaceId = null, ObjectId? blobId = null)
        {
            UserWorkspaceFilesDto workSpaceDetails = await this
                .GetUserWorkSpaceDetails(workSpaceId, userId, blobId);

            // Check if there are no workspaces and set a flag
            if (workSpaceDetails.Workspaces == null || workSpaceDetails.Workspaces.Count == 0)
            {
                TempData["NoWorkspace"] = true;
                return RedirectToAction("Create", "WorkSpace", new { userId });
            }

            return View(workSpaceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> JsonFormatter(UserWorkspaceFilesDto userWorkspaceFiles)
        {
            UserWorkspaceFilesDto userWorkspaceFilesDto = await this
                .GetUserWorkSpaceDetails(userWorkspaceFiles.WorkspaceId.Value, userWorkspaceFiles.UserId.Value);

            if (userWorkspaceFiles.Blob == null)
            {
                TempData["Error"] = Resource.CN_Error_1001;
                return View(userWorkspaceFilesDto);
            }

            BlobDto result = await _formatterServices.JsonValidate(userWorkspaceFiles.Blob);
            if (result!=null)
            {
                TempData["Success"] = Resource.CN_Success_1001;
                userWorkspaceFilesDto.Blob = result;
                return View(userWorkspaceFilesDto);
            }

            userWorkspaceFilesDto.Blob = userWorkspaceFiles.Blob;
            TempData["Error"] = Resource.CN_Error_1001;
            return View(userWorkspaceFilesDto); 
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserWorkspaceFilesDto userWorkspaceDetail, string filename, string? Name, string? Description)
        {
            if (userWorkspaceDetail.WorkspaceId == null)
            {
                WorkspacesDto workspace = new()
                {
                    Name = Name,
                    Description = Description,
                };
                WorkspacesDto result = await _workspaceService
                    .CreateWorkspace(workspace, userWorkspaceDetail.UserId.Value);
                userWorkspaceDetail.WorkspaceId = result.Id;
            }

            bool jsonResult = await _formatterServices
                .Save(userWorkspaceDetail.Blob, userWorkspaceDetail.WorkspaceId.Value, userWorkspaceDetail.UserId.Value, filename);
            if (jsonResult)
            {
                TempData.Clear();
                TempData["Success"] = Resource.CN_Success_1001;

                return Json(new { success = true });
            }

            TempData["Error"] = Resource.CN_Error_1001;
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserWorkspaceFilesDto userWorkspaceDetail)
        {
            BlobDto result = await _formatterServices
                .Update(blobDto: userWorkspaceDetail.Blob, blobID: userWorkspaceDetail.BlobId.Value, userId: userWorkspaceDetail.UserId.Value);
            if (result != null)
            {
                TempData["Success"] = Resource.CN_Update_1001;
                return RedirectToAction("JsonFormatter", new
                {
                    userId = userWorkspaceDetail.UserId.Value,
                    workSpaceId = userWorkspaceDetail.WorkspaceId.Value,
                });
            }

            TempData["Error"] = Resource.CN_Error_1002;
            return RedirectToAction("JsonFormatter", new
            {
                userId = userWorkspaceDetail.UserId.Value,
                workSpaceId = userWorkspaceDetail.WorkspaceId.Value,
                blobId = result.Id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Javascript(ObjectId userId, ObjectId? workSpaceId = null, ObjectId? blobId = null)
        {
            UserWorkspaceFilesDto workSpaceDetails = await this.GetUserWorkSpaceDetails(workSpaceId, userId, blobId);

            // Check if there are no workspaces and set a flag
            if (workSpaceDetails.Workspaces == null || workSpaceDetails.Workspaces.Count == 0)
            {
                TempData["NoWorkspace"] = true;
                return RedirectToAction("Create", "WorkSpace", new { userId });
            }

            return View(workSpaceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Javascript(UserWorkspaceFilesDto userWorkspaceFiles)
        {

            UserWorkspaceFilesDto userWorkspaceFilesDto = await this
                .GetUserWorkSpaceDetails(userWorkspaceFiles.WorkspaceId.Value, userWorkspaceFiles.UserId.Value);
            
            if (userWorkspaceFiles.Blob == null)
            {
                TempData["Error"] = Resource.CN_Error_1001;
                return View(userWorkspaceFilesDto);
            }
            
            BlobDto result = await _formatterServices.JavascriptValidate(userWorkspaceFiles.Blob);
            if (result.Input != null || result.Output != null)
            {
                TempData["Success"] = Resource.CN_Success_1001;
                userWorkspaceFilesDto.Blob = result;
                return View(userWorkspaceFilesDto);
            }

            TempData["Error"] = Resource.CN_Error_1001;
            return View(userWorkspaceFiles);
        }
    }
}
