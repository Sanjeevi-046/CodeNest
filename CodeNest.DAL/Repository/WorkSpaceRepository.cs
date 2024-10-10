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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CodeNest.DAL.Repository
{
    public class WorkSpaceRepository : IWorkSpaceRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMapper _mapper;
        public WorkSpaceRepository(MongoDbService mongoDbService, IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
        }
        public async Task<List<WorkspacesDto>> GetWorkspaces(ObjectId userId)
        {
            // Query Workspaces created by the specific userId
            List<Workspaces> workspaces = await _mongoDbService.WorkSpaces
                .AsQueryable()
                .Where(w => w.CreatedBy == userId)
                .ToListAsync();

            return _mapper.Map<List<WorkspacesDto>>(workspaces);
        }

        /// <summary>
        /// Fetches the Workspace and BlobDatas 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> returns the </returns>
        public async Task<List<UserWorkspaceFilesDto>> GetUserWorkSpace(ObjectId userId)
        {
            // Query Workspaces created by the specific userId
            List<Workspaces> workspaces = await _mongoDbService.WorkSpaces
                .AsQueryable()
                .Where(w => w.CreatedBy == userId)
                .ToListAsync();

            // Get workspace IDs for fetching blob data
            List<ObjectId> workspaceIds = workspaces.Select(w => w.Id).ToList();

            // Query BlobData related to the workspace IDs
            List<BlobData> blobs = await _mongoDbService.BlobDatas
                .AsQueryable()
                .Where(b => workspaceIds.Contains(b.Workspaces))
                .ToListAsync();

            // Create a list of UserWorkspaceFilesDto using LINQ query syntax
            List<UserWorkspaceFilesDto> UserWorkSpace =  (from workspace in workspaces
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

            return UserWorkSpace;
        }
        /// <summary>
        /// Inserting the Newly Created WorkSpaces 
        /// </summary>
        /// <param name="workspacesDto"></param>
        /// <param name="user"></param>
        /// <returns>returns the inserted Workspaces in <see cref="WorkspacesDto"/> else <see cref="WorkspacesDto=null"/></returns>
        public async Task<WorkspacesDto> CreateWorkspace(WorkspacesDto workspacesDto, ObjectId user)

        {
            try
            {
                Workspaces workspaces = new()
                {
                    Name = workspacesDto.Name,
                    Description = workspacesDto.Description,
                    CreatedBy = user,
                    CreatedOn = DateTime.UtcNow
                };
                await _mongoDbService.WorkSpaces
                    .InsertOneAsync(workspaces);
                return _mapper.Map<WorkspacesDto>(workspaces);
            }
            catch
            {
                return new WorkspacesDto();
            }
        }
    }
}
