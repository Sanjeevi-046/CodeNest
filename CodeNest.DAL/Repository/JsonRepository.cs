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

namespace CodeNest.DAL.Repository
{
    public class JsonRepository : IJsonRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMapper _mapper;

        public JsonRepository(MongoDbService mongoDbService , IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;

        }
        public async Task<List<BlobDto>> GetJsonList(ObjectId workspaceId)
        {
            List<BlobData> jsonData = await _mongoDbService.BlobDatas
                .Find(x=>x.Workspaces == workspaceId).ToListAsync();
            return _mapper.Map<List<BlobDto>>(jsonData);
        }
        public async Task<bool> SaveAsync(BlobDto jsonData, ObjectId workSpace, ObjectId user)
        {
            Workspaces workspaceName = await _mongoDbService.WorkSpaces
                .Find(x => x.Id == workSpace).FirstOrDefaultAsync();
            BlobData jsonUser = new()
            {
                Name = workspaceName.Name,
                Input = jsonData.Input,
                Output = jsonData.Output,
                Type = "Json",
                Workspaces = workSpace,
                CreatedBy = user,
                CreatedOn = DateTime.UtcNow
            };
            try
            {
                await _mongoDbService.BlobDatas.InsertOneAsync(jsonUser);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
