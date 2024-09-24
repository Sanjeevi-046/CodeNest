using CodeValidator.BLL.Service;
using CodeValidator.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeValidator.UI.Controllers
{
    public class JsonController : Controller
    {
        private readonly IJsonService _jsonService;
        public JsonController(IJsonService jsonService) 
        {
            _jsonService = jsonService;
        }
        public IActionResult Index(JsonModelDto jsonModel = null)
        {
            return View(jsonModel);
        }
        [HttpGet]
        public async Task<IActionResult> Validate(JsonModelDto jsonModel = null)
        {
            return View(jsonModel);
        }
        [HttpPost]
        public async Task<IActionResult> Validate(string JsonInput)
        {
            var result = await _jsonService.Validate(JsonInput);
            if (result.IsValid)
            {
                ViewBag.Success = result.Message;
                return View(result.jsonModelDto);
            } 
            ViewBag.ErrorMessage = result.Message;
            return View( result.jsonModelDto);
        }
    }
}
