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
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Bson;
namespace CodeNest.UI.Controllers
{
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
            List<WorkspacesDto> workspaces = new();
            ObjectId workspaceObjectId = ObjectId.Empty;
            WorkspacesDto workspace = new();
            List<BlobDto> blobsList = new();

            // Fetch workspaces for the given userId
            workspaces = await _workspaceService.GetWorkspaces(userId);

            // Determine the workspace to use
            if (workSpaceId != null && workSpaceId != ObjectId.Empty)
            {
                workspaceObjectId = workSpaceId.Value;
            }
            else if (workspaces.Any())
            {
                // Get the latest workspace based on some criteria, e.g., last modified date
                workspace = workspaces.OrderByDescending(w => w.CreatedOn).FirstOrDefault();
                workspaceObjectId = workspace?.Id ?? ObjectId.Empty;
            }

            // Fetch the workspace details and blobs if a valid workspaceObjectId is found
            if (workspaceObjectId != ObjectId.Empty)
            {
                workspace = await _workspaceService.GetWorkspace(workspaceObjectId);
                blobsList = await _jsonService.GetJson(workspaceObjectId);
            }

            // Fetch the blob data if blobId is provided
            BlobDto blob = new();
            if (blobId != null)
            {
                blob = await _formatterServices.GetBlob(blobId.Value);
            }
            else if (blobsList.Any())
            {
                // Get the latest blob based on some criteria, e.g., creation date
                blob = blobsList.OrderByDescending(b => b.CreatedOn).FirstOrDefault();
            }

            // Create the UserWorkspaceFilesDto object
            UserWorkspaceFilesDto userWorkspace = new()
            {
                UserId = userId,
                WorkspaceName = workspace?.Name,
                WorkspaceId = workspaceObjectId,
                Workspaces = workspaces,
                BlobsList = blobsList,
                Blob = blob
            };

            return userWorkspace;
        }

        public async Task<IActionResult> JsonFormatter(ObjectId userId, ObjectId? workSpaceId=null , ObjectId? blobId = null)
        {
            UserWorkspaceFilesDto workSpaceDetails = await this.GetUserWorkSpaceDetails(workSpaceId, userId,blobId);
            return View(workSpaceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> JsonFormatter(UserWorkspaceFilesDto userWorkspaceFiles)
        {
            UserWorkspaceFilesDto userWorkspaceFilesDto = await this
                .GetUserWorkSpaceDetails(userWorkspaceFiles.WorkspaceId.Value, userWorkspaceFiles.UserId.Value);
            if (userWorkspaceFiles.Blob == null)
            {
                TempData["Error"] = "Invalid JSON data.";
                return View(userWorkspaceFilesDto); 
            }

            ValidationDto result = await _formatterServices.JsonValidate(userWorkspaceFiles.Blob);
            if (result.IsValid)
            {
                TempData["Success"] = result.Message;
                userWorkspaceFilesDto.Blob = result.Blobs;

                return View(userWorkspaceFilesDto); 
            }
            userWorkspaceFilesDto.Blob = userWorkspaceFiles.Blob;
            TempData["Error"] = result.Message;
            return View(userWorkspaceFilesDto); // Pass the original BlobDto to the view
        }

        [HttpPost]
        public async Task<IActionResult> SaveJson(UserWorkspaceFilesDto userWorkspaceDetail, string? Name, string? Description)
        {
            
            if (userWorkspaceDetail.WorkspaceId == null)
            {
                WorkspacesDto workspace = new()
                {
                    Name = Name,
                    Description = Description,
                };
                WorkspacesDto result = await _workspaceService.CreateWorkspace(workspace, userWorkspaceDetail.UserId.Value);
                userWorkspaceDetail.WorkspaceId = result.Id;
            }

            bool jsonResult = await _formatterServices
                .Save(userWorkspaceDetail.Blob,userWorkspaceDetail.WorkspaceId.Value , userWorkspaceDetail.UserId.Value);
            if (jsonResult)
            {
                TempData["Success"] = "Success";
                return RedirectToAction("JsonFormatter", "Formatter", 
                    new { userId=userWorkspaceDetail.UserId, workSpaceId=userWorkspaceDetail.WorkspaceId });
            }
            TempData["Error"] = "Error Occured";
            return RedirectToAction("JsonFormatter", "Formatter",
                    new { userId = userWorkspaceDetail.UserId, workSpaceId = userWorkspaceDetail.WorkspaceId });
        }
    }
}
