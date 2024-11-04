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

using CodeNest.DTO.CommonDto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CodeNest.DTO.Models
{
    public class WorkspacesDto : AuditDto
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [RegularExpression(@"^[A-Za-z\d\s]{5,}$", ErrorMessage = "Surname must be alphabetic characters and Numeric.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
