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

using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CodeNest.UI.ViewComponents
{
    [ViewComponent(Name = "Json")]
    public class JsonViewComponent : ViewComponent
    {
        private readonly IJsonService _jsonService;

        public JsonViewComponent(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }
        public async Task<IViewComponentResult> InvokeAsync(ObjectId workspaceId)
        {
            if (workspaceId != null)
            {
                List<BlobDto> jsonData = await _jsonService.GetJson(workspaceId);
                return View("Default", jsonData); // Return the Default view with the jsonData model
            }

            return View("Default", new List<DTO.Models.BlobDto>()); // Return empty list if workspaceId is null or empty
        }
    }
}
