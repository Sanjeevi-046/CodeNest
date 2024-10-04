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
       
        public JsonRepository(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
            
        }

        public async Task<bool> SaveAsync(JsonDto jsonData , ObjectId workSpace , ObjectId user)
        {
            Workspaces workspaceName = await _mongoDbService.WorkSpaces.Find(x => x.Id == workSpace).FirstOrDefaultAsync();
            CustomJson jsonUser = new ()
            {
                Name = workspaceName.Name + ".json",
                JsonInput = jsonData.JsonInput,
                JsonOutput = jsonData.JsonOutput,
                Workspaces = workSpace,
                CreatedBy = user,
                CreatedOn = DateTime.UtcNow
            };
            try
            {
                await _mongoDbService.Json.InsertOneAsync(jsonUser);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
