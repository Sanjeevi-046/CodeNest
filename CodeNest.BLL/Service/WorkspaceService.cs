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

using AutoMapper;
using CodeNest.DAL.Context;
using CodeNest.DAL.Models;
using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
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
            _logger.LogInformation("GetWorkspaces: Retrieving workspaces for user.");

            try
            {
                List<WorkspacesDto> userWorkspace = await _workSpaceRepository.GetWorkspaces(user);
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
            _logger.LogInformation("CreateWorkspace: Creating a new workspace.");

            try
            {
                WorkspacesDto workSpace = await _workSpaceRepository.CreateWorkspace(workspacesDto, user);
                if (workSpace != null)
                {
                    _logger.LogInformation("CreateWorkspace: Successfully created workspace.");
                    return workSpace;
                }

                _logger.LogWarning("CreateWorkspace: Workspace creation returned null.");
                return new WorkspacesDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateWorkspace: An error occurred while creating workspace.");
                throw;
            }
        }
    }
}
