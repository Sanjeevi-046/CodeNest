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

namespace CodeNest.DAL.Models
{
    public class Users
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("Password")]
        public string? Password { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("FirstName")]
        public string? FirstName { get; set; }
        [BsonElement("LastName")]
        public string? LastName { get; set; }

        [BsonElement("MobileNumber")]
        public string? MobileNumber { get; set; }

        [BsonElement("Country")]
        public string? Country { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? Workspaces { get; set; }
    }
}
