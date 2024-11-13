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

namespace CodeNest.DTO.Models
{
    public class UserWorkspaceFilesDto
    {
        public ObjectId? UserId { get; set; }
        public string? UserName { get; set; }
        public ObjectId? WorkspaceId { get; set; }
        public String? WorkspaceName { get; set; }
        public ObjectId? BlobId { get; set; }
        public List<BlobDto>? BlobsList { get; set; }
        public List<WorkspacesDto>? Workspaces { get; set; }
        public BlobDto? Blob { get; set; }
        public WorkspacesDto? Workspace { get; set; }
    }
}
