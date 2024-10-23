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

using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
namespace CodeNest.BLL.Service
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkSpaceRepository _workSpaceRepository;
        private readonly ILogger<WorkspaceService> _logger;

        public WorkspaceService(IWorkSpaceRepository workSpaceRepository, ILogger<WorkspaceService> logger)
        {
            _workSpaceRepository = workSpaceRepository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the list of workspaces for a given user.
        /// </summary>
        /// <param name="user">The user identifier.</param>
        /// <returns>A list of workspaces.</returns>
        public async Task<List<WorkspacesDto>> GetWorkspaces(ObjectId user)
        {
            try
            {
                List<WorkspacesDto> userWorkspace = await _workSpaceRepository
                    .GetWorkspaces(user);
                _logger.LogInformation("GetWorkspaces: Successfully retrieved workspaces.");
                return userWorkspace;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetWorkspaces: An error occurred while retrieving workspaces.");
                throw;
            }
        }

        /// <summary>
        /// Creates a new workspace for a given user.
        /// </summary>
        /// <param name="workspacesDto">The workspace details.</param>
        /// <param name="user">The user identifier.</param>
        /// <returns>The created workspace details.</returns>
        public async Task<WorkspacesDto> CreateWorkspace(WorkspacesDto workspacesDto, ObjectId user)
        {
                WorkspacesDto workSpace = await _workSpaceRepository.CreateWorkspace(workspacesDto, user);
                return workSpace;
        }

        public async Task<WorkspacesDto> GetWorkspace(ObjectId id)
        {
            List<WorkspacesDto> workspaces = await _workSpaceRepository.GetWorkspaces(id);
            WorkspacesDto workspace = workspaces.FirstOrDefault();
            return workspace;
        }
        public async Task<WorkspacesDto> GetWorkspaceByName(ObjectId userId, string name)
        {
            WorkspacesDto workspace = await _workSpaceRepository.GetWorkspacebyName(name);
            return workspace;
        }
    }
}
