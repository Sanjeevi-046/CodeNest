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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeNest.DAL.Repository
{
    public class WorkSpaceRepository : IWorkSpaceRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMapper _mapper;
        public WorkSpaceRepository(MongoDbService mongoDbService , IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
        }
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
