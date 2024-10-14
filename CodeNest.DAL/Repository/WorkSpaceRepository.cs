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
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CodeNest.DAL.Repository
{
    public class WorkSpaceRepository : IWorkSpaceRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkSpaceRepository> _logger;

        public WorkSpaceRepository(MongoDbService mongoDbService, IMapper mapper, ILogger<WorkSpaceRepository> logger)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the list of workspaces created by a specific user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A list of workspaces.</returns>
        public async Task<List<WorkspacesDto>> GetWorkspaces(ObjectId userId)
        {
            _logger.LogInformation("GetWorkspaces: Retrieving workspaces for user.");

            try
            {
                List<Workspaces> workspaces = await _mongoDbService.WorkSpaces
                    .AsQueryable()
                    .Where(w => w.CreatedBy == userId)
                    .ToListAsync();

                _logger.LogInformation("GetWorkspaces: Successfully retrieved workspaces.");
                return _mapper.Map<List<WorkspacesDto>>(workspaces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetWorkspaces: An error occurred while retrieving workspaces.");
                throw;
            }
        }

        /// <summary>
        /// Fetches the workspaces and associated blob data for a specific user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>A list of user workspace files.</returns>
        public async Task<List<UserWorkspaceFilesDto>> GetUserWorkSpace(ObjectId userId)
        {
            _logger.LogInformation("GetUserWorkSpace: Retrieving user workspaces and associated blob data.");

            try
            {
                List<Workspaces> workspaces = await _mongoDbService.WorkSpaces
                    .AsQueryable()
                    .Where(w => w.CreatedBy == userId)
                    .ToListAsync();

                List<ObjectId> workspaceIds = workspaces.Select(w => w.Id).ToList();

                List<BlobData> blobs = await _mongoDbService.BlobDatas
                    .AsQueryable()
                    .Where(b => workspaceIds.Contains(b.Workspaces))
                    .ToListAsync();

                List<UserWorkspaceFilesDto> userWorkSpace = (from workspace in workspaces
                                                             select new UserWorkspaceFilesDto
                                                             {
                                                                 WorkspaceId = workspace.Id,
                                                                 WorkspaceName = workspace.Name,
                                                                 WorkspaceDescription = workspace.Description,
                                                                 Blobs = blobs
                                                                     .Where(b => b.Workspaces == workspace.Id)
                                                                     .Select(blob => new BlobDto
                                                                     {
                                                                         Id = blob.Id,
                                                                         Name = blob.Name,
                                                                         Input = blob.Input,
                                                                         Output = blob.Output,
                                                                         Type = blob.Type,
                                                                         Version = blob.Version
                                                                     }).ToList()
                                                             }).ToList();

                _logger.LogInformation("GetUserWorkSpace: Successfully retrieved user workspaces and blob data.");
                return userWorkSpace;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserWorkSpace: An error occurred while retrieving user workspaces and blob data.");
                throw;
            }
        }

        /// <summary>
        /// Inserts a newly created workspace.
        /// </summary>
        /// <param name="workspacesDto">The workspace details.</param>
        /// <param name="user">The user identifier.</param>
        /// <returns>The inserted workspace details if successful, otherwise null.</returns>
        public async Task<WorkspacesDto?> CreateWorkspace(WorkspacesDto workspacesDto, ObjectId user)
        {
            _logger.LogInformation("CreateWorkspace: Attempting to create a new workspace.");

            try
            {
                Workspaces workspaces = new()
                {
                    Name = workspacesDto.Name,
                    Description = workspacesDto.Description,
                    CreatedBy = user,
                    CreatedOn = DateTime.UtcNow
                };

                await _mongoDbService.WorkSpaces.InsertOneAsync(workspaces);
                _logger.LogInformation("CreateWorkspace: Successfully created new workspace.");
                return _mapper.Map<WorkspacesDto>(workspaces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateWorkspace: An error occurred while creating the workspace.");
                throw;
            }
        }
    }
}
