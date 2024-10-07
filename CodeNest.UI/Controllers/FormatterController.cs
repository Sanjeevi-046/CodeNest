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
using CodeNest.UI.Models.JsonViewModel;
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
        public async Task<IActionResult> Validate(JsonValidationViewModel model)
        {
          
                ValidationDto result = await _formatterServices.JsonValidate(model.JsonInput);
                model.IsValid = result.IsValid;
                model.Message = result.Message;
                model.JsonDto = result.JsonDto;

                return View("JsonFormatter", model);
            return View("JsonFormatter", model);
        }
    }
}
