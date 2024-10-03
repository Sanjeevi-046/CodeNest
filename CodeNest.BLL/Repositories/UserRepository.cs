// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: Sample Description.
//
// ***********************************************************************************************

using AutoMapper;
using CodeNest.DAL.Context;
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;
using MongoDB.Driver;

namespace CodeNest.BLL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MangoDbService _mangoDbService;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class, which handles user-related operations.
        /// </summary>
        /// <param name="mangoDbService"> for interacting with mongo database </param>
        /// <param name="mapper"> used for mapping the DTO's and Domain Model </param>
        public UserRepository(MangoDbService mangoDbService, IMapper mapper)
        {
            _mangoDbService = mangoDbService;
            _mapper = mapper;
        }
        /// <summary>
        /// Getting the User detail by their ID
        /// </summary>
        /// <param name="id"> User's Id - will be in Mongo ObjectId type </param>
        /// <returns> returns User detail in <see cref="UsersDto"></returns>
        public async Task<UsersDto> GetUserById(string id)
        {
            Users existingUser = await _mangoDbService.UserModel
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(existingUser);
        }
        /// <summary>
        /// Validating the user details 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> If the User detail is in Database means returns the detail in <see cref="UsersDto"> else retrurns <see cref="UsersDto==null"/></returns>
        public async Task<UsersDto> Login(string username, string password)
        {
            Users user = await _mangoDbService.UserModel
               .Find(u => u.Name == username && u.Password == password)
               .FirstOrDefaultAsync();

            return _mapper.Map<UsersDto>(user);
        }
        /// <summary>
        /// Adding the User in mongo Database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<UsersDto> Register(UsersDto newUser)
        {
            Users existingUser = await _mangoDbService.UserModel
                .Find(u => u.Name == newUser.Name)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                return null;
            }

            await _mangoDbService.UserModel.InsertOneAsync(_mapper.Map<Users>(newUser));
            return newUser;

        }
    }
}
