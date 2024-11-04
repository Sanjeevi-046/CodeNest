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

namespace CodeNest.DTO.Models
{
    public class ValidationDto
    {
        public bool IsValid { get; set; }
        public string? Message { get; set; }
        public BlobDto? Blobs { get; set; }
    }
}
