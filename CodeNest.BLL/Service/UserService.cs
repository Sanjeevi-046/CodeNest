using AutoMapper;
using CodeNest.DAL.Context;
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;
using MongoDB.Driver;

namespace CodeNest.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly MangoDbService _mangoDbService;
        private readonly IMapper _mapper;
        public UserService(MangoDbService mangoDbService, IMapper mapper)
        {
            _mangoDbService = mangoDbService;
            _mapper = mapper;
        }

        public async Task<UsersDto> GetUserById(string id)
        {
            Users existingUser = await _mangoDbService.UserModel
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(existingUser);
        }

        public async Task<UsersDto> Login(string username, string password)
        {
            Users user = await _mangoDbService.UserModel
               .Find(u => u.Name == username && u.Password == password)
               .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(user);
        }
        public async Task<bool> Register(UsersDto newUser)
        {
            Users existingUser = await _mangoDbService.UserModel
                .Find(u => u.Name == newUser.Name)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return false;
            }

            await _mangoDbService.UserModel.InsertOneAsync(_mapper.Map<Users>(newUser));
            return true;
        }
    }
}
