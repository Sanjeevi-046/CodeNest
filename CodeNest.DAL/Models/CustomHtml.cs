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

using CodeNest.DAL.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeNest.DAL.Models
{
    public class CustomHtml : Audit
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 
        [BsonElement("Name")]
        public string? Name { get; set; }
        public string? HtmlInput { get; set; }
        public string? HtmlOutput { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Workspaces { get; set; }

        public string? Version { get; set; }
    }
}
