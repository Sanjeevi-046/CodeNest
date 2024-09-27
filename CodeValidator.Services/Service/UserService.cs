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
        public UserService(MangoDbService mangoDbService, IMapper mapper)
        {
            _mangoDbService = mangoDbService;
            _mapper = mapper;
        }

        public async Task<UsersDto> GetUserById(string id)
        {
            var existingUser = await _mangoDbService.userModel
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(existingUser);
        }

        public async Task<UsersDto> Login(string username, string password)
        {
            var user = await _mangoDbService.userModel
               .Find(u => u.Name == username && u.Password == password)
               .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(user);
        }
        public async Task<bool> Register(UsersDto newUser)
        {
            var existingUser = await _mangoDbService.userModel
                .Find(u => u.Name == newUser.Name)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return false;
            }
            
            await _mangoDbService.userModel.InsertOneAsync(_mapper.Map<Users>(newUser));
            return true;
        }
    }
}
