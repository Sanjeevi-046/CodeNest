using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.Service
{
    public interface IUserService
    {
        Task<bool> Login(string username, string password);
        Task<bool> Register(UserDto newUser);
    }
}
