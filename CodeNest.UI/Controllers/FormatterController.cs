using CodeNest.BLL.Service;
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            WorkspacesDto? workspace = null;
            List<BlobDto> blobsList = new();
            BlobDto? blob = null;
            // Fetch workspaces for the given userId
            workspaces = await _workspaceService.GetWorkspaces(userId);

            // Determine the workspace to use
            if (workSpaceId != null && workSpaceId != ObjectId.Empty)
            {
                workspaceObjectId = workSpaceId.Value;
            }
            else if (workspaces.Any())
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
            UserWorkspaceFilesDto workSpaceDetails = await this.GetUserWorkSpaceDetails(workSpaceId, userId, blobId);

            // Check if there are no workspaces and set a flag
            if (workSpaceDetails.Workspaces == null || !workSpaceDetails.Workspaces.Any())
            {
                TempData["NoWorkspace"] = true;
                return RedirectToAction("Create", "WorkSpace", new { userId = userId });
            }

            return View(workSpaceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> JsonFormatter(UserWorkspaceFilesDto userWorkspaceFiles)
        {
            if (userWorkspaceFiles.WorkspaceId == null || userWorkspaceFiles.UserId == null)
            {
                TempData["Error"] = "WorkspaceId or UserId is null.";
                return View(userWorkspaceFiles);
            }

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
        public async Task<IActionResult> SaveJson(UserWorkspaceFilesDto userWorkspaceDetail, string filename , string? Name, string? Description)
        {
            if (userWorkspaceDetail.UserId == null)
            {
                TempData["Error"] = "UserId is null.";
                return Json(new { success = false, message = "UserId is null." });
            }

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
                .Save(userWorkspaceDetail.Blob, userWorkspaceDetail.WorkspaceId.Value, userWorkspaceDetail.UserId.Value, filename);
            if (jsonResult)
            {
                TempData["Success"] = "JSON saved successfully.";

                return Json(new { success = true});
            }
            TempData["Error"] = "Error occurred while saving JSON.";
            return Json(new { success = false, message = "Error occurred while saving JSON." });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserWorkspaceFilesDto userWorkspaceDetail)
        {
            BlobDto result = await _formatterServices.Update(blobDto: userWorkspaceDetail.Blob, blobID: userWorkspaceDetail.BlobId.Value, userId: userWorkspaceDetail.UserId.Value);
            if (result != null) 
            {
                TempData["Success"] = "Saved successfully!";
                return RedirectToAction("JsonFormatter", new {
                    userId=userWorkspaceDetail.UserId.Value,
                    workSpaceId=userWorkspaceDetail.WorkspaceId.Value,
                });
            }
            TempData["Error"] = "Error occured while saving!";
            return RedirectToAction("JsonFormatter", new
            {
                userId = userWorkspaceDetail.UserId.Value,
                workSpaceId = userWorkspaceDetail.WorkspaceId.Value,
                blobId = result.Id
            });
        }
    }
}
