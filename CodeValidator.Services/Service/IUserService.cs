using CodeValidator.DTO.Models;

namespace CodeValidator.BLL.Service
{
    public interface IUserService
    {
        Task<UsersDto> GetUserById(string id);
        Task<UsersDto> Login(string username, string password);
        Task<bool> Register(UsersDto newUser);
    }
}
