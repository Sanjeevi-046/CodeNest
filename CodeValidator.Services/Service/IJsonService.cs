using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.Service
{
    public interface IJsonService
    {

        Task<ValidationDto> Validate(string jsonObject);
    }
}
