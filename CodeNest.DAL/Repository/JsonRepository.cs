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

        public JsonRepository(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;

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
