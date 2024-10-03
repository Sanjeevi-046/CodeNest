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

using CodeNest.DTO.CommonDto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DTO.Models
{
    public class BaseToStringDto : AuditDto
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string? BaseInput { get; set; }
        public string? StringOutput { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Workspaces { get; set; }
        public string? Version { get; set; } // 0.0.0 For each update we need to increment - 0.0.1
    }
}
