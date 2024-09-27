﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeValidator.DAL.Models
{
    public class Workspaces
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedOn { get; set; }

    }
}
