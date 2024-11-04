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

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DTO.CommonDto
{
    public class AuditDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
