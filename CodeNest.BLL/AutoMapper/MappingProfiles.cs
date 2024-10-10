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
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;

namespace CodeNest.BLL.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            _ = CreateMap<UsersDto, Users>().ReverseMap();
            _ = CreateMap<Workspaces, WorkspacesDto>().ReverseMap();
            _ = CreateMap<BlobDto, BlobData>().ReverseMap();
           
        }
    }
}
