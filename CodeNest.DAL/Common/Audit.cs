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

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DAL.Common
{
    public class Audit
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
