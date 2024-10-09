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
using MongoDB.Bson;
using MongoDB.Driver;
namespace CodeNest.BLL.Service
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkSpaceRepository _workSpaceRepository;

        public WorkspaceService(IWorkSpaceRepository workSpaceRepository)
        {
            _workSpaceRepository = workSpaceRepository;
        }
        public async Task<List<WorkspacesDto>> GetWorkspaces(ObjectId user)
        {
            List<WorkspacesDto> workspaces = await _workSpaceRepository.GetWorkspaces(user);
            return workspaces;
        }

        public async Task<WorkspacesDto> CreateWorkspace(WorkspacesDto workspacesDto, ObjectId user)
        {
            WorkspacesDto workSpace = await _workSpaceRepository.CreateWorkspace(workspacesDto, user);
            if (workSpace != null)
            {
                return workSpace;
            }
            return new WorkspacesDto();
        }
    }
}
