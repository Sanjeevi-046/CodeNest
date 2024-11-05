// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

using CodeNest.DTO.Models;
using MongoDB.Bson;

namespace CodeNest.BLL.Service
{
    public interface IFormatterServices
    {
        Task<BlobDto> JsonValidate(BlobDto jsonDto);
        Task<bool> Save(BlobDto jsonDto, ObjectId workSpace, ObjectId user, string filename);
        Task<BlobDto> GetBlob(ObjectId blobId);
        Task<BlobDto> Update(BlobDto blobDto, ObjectId blobID, ObjectId userId);
        Task<BlobDto> JavascriptValidate(BlobDto blobDto);
    }
}
