using AutoMapper;
using CodeValidator.DAL.Context;
using CodeValidator.DAL.Models;
using CodeValidator.DTO.Models;

using MongoDB.Driver;

namespace CodeValidator.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly MangoDbService _mangoDbService;
        private readonly IMapper _mapper;
        public UserService(MangoDbService mangoDbService , IMapper mapper) 
        {
            _mangoDbService = mangoDbService;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var existingUser = await _mangoDbService.userModel
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _mangoDbService.userModel
               .Find(u => u.Name == username && u.Password == password)
               .FirstOrDefaultAsync();

            return _mapper.Map<UserDto>(user);
        }
        public async Task<bool> Register(UserDto newUser)
        {
            var existingUser = await _mangoDbService.userModel
                .Find(u => u.Name == newUser.Name)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return false; 
            }
            newUser.RegisterDate = DateTime.Now.ToString();
            await _mangoDbService.userModel.InsertOneAsync(_mapper.Map<User>(newUser));
            return true;
        }
    }
}
