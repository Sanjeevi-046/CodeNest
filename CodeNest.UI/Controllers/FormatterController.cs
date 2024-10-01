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
            var result = await _formatterServices.JsonValidate(JsonInput);
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
