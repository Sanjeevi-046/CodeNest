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

using CodeNest.DTO.Models;

namespace CodeNest.UI.Models.JsonViewModel
{
    public class JsonValidationViewModel
    {
        public string JsonInput { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }
        public JsonDto? JsonDto { get; set; }
    }
}
