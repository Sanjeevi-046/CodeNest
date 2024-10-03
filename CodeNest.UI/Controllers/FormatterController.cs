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

using CodeNest.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeNest.UI.Controllers
{
    public class FormatterController : Controller
    {
        private readonly IFormatterServices _formatterServices;
        public FormatterController(IFormatterServices formatterServices)
        {
            _formatterServices = formatterServices;
        }
        public IActionResult JsonFormatter() => View();

        [HttpPost]
        public async Task<IActionResult> Validate(string JsonInput)
        {
            DTO.Models.ValidationDto result = await _formatterServices.JsonValidate(JsonInput);
            if (result.IsValid)
            {
                ViewBag.Success = result.Message;
                return View(result.jsonDto);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(result.jsonDto);
        }

    }
}
