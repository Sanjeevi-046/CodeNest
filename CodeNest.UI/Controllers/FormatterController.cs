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

using Microsoft.AspNetCore.Mvc;
using CodeNest.DTO.Models;
using CodeNest.DAL.Repository;
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
            ValidationDto result = await _formatterServices.JsonValidate(JsonInput);
            if (result.IsValid)
            {
                ViewBag.Success = result.Message;
                return View(result.JsonDto);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(result.JsonDto);
        }
    }
}
