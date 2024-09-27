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
        public IActionResult Index(JsonDto jsonModel = null)
        {
            return View(jsonModel);
        }
        [HttpGet]
        public async Task<IActionResult> Validate(JsonDto jsonModel = null)
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
                return View(result.jsonDto);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(result.jsonDto);
        }
    }
}
