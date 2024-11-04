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

using CodeNest.DAL.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DAL.Models
{
    public class BlobData : Audit
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }
        public string Type { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Workspaces { get; set; }
        public string? Version { get; set; }
    }
}
