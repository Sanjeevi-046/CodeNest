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

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeNest.DTO.CommonDto
{
    public class FieldDto:AuditDto
    {
        public string? Name { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Workspaces { get; set; }
        public string? Version { get; set; }
    }
}
