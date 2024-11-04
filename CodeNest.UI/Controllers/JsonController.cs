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

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CodeNest.UI.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index(ObjectId workspace)
        {
            return ViewComponent("Json", new { workspaceId = workspace });
        }
    }
}
