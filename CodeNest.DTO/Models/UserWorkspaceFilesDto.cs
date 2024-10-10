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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeNest.DTO.Models
{
    public class UserWorkspaceFilesDto
    {
        public ObjectId WorkspaceId { get; set; }
        public string WorkspaceName { get; set; }
        public string WorkspaceDescription { get; set; }
        public List<BlobDto> Blobs { get; set; } = new List<BlobDto>();
    }
}
