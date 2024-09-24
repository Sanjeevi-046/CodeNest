using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.Service
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id);
        Task<UserDto> Login(string username, string password);
        Task<bool> Register(UserDto newUser);
    }
}
