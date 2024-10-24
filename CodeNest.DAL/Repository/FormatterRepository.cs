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
    public class FormatterRepository : IFormatterRepository
    {
        private readonly MongoDbService _mongoDbService;
        private readonly IMapper _mapper;
        public FormatterRepository(MongoDbService mongoDbService , IMapper mapper) 
        {
            _mapper = mapper;
            _mongoDbService = mongoDbService;
        }
        public async Task<BlobDto> GetBlob(ObjectId blobId)
        {
            BlobData blobData = await _mongoDbService.BlobDatas
                .Find(x => x.Id == blobId).FirstOrDefaultAsync();
            return _mapper.Map<BlobDto>(blobData);
        }
        public async Task<BlobDto> Update(BlobDto blobDto, ObjectId blobID ,ObjectId userId)
        {
            UpdateDefinition<BlobData> updateDefinition = Builders<BlobData>.Update
                        .Set(x => x.Input, blobDto.Input)
                        .Set(x => x.Output, blobDto.Output)
                        .Set(x => x.ModifiedBy, userId)
                        .Set(x => x.ModifiedOn, DateTime.UtcNow);

            UpdateResult result = await _mongoDbService.BlobDatas
                .UpdateOneAsync(x => x.Id == blobID, updateDefinition);

            BlobDto blobData = await this.GetBlob(blobID);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return _mapper.Map<BlobDto>(blobData);
            }
            return new BlobDto();   
        }
    }
}
