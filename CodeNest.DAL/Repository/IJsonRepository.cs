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

using CodeNest.DTO.Models;
using MongoDB.Bson;

namespace CodeNest.DAL.Repository
{
    public interface IJsonRepository
    {
        Task<bool> SaveAsync(BlobDto jsonData, ObjectId workSpace, ObjectId user, string filename);
        Task<List<BlobDto>> GetJsonList(ObjectId workspaceId);
    }
}
