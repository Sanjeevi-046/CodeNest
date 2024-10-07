// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: 
//
// ***********************************************************************************************

using AutoMapper;
using CodeNest.DAL.Context;
using CodeNest.DAL.Models;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace CodeNest.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbService _mangoDbService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(MongoDbService mangoDbService, IMapper mapper, ILogger<UserRepository> logger)
        {
            _mangoDbService = mangoDbService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UsersDto?> GetUserById(string id)
        {
            try
            {
                Users existingUser = await _mangoDbService.UserModel
                    .Find(u => u.Id.ToString() == id)
                    .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return null;
                }

                return _mapper.Map<UsersDto>(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the user");
                throw;
            }
        }

        public async Task<UsersDto?> Login(string username, string password)
        {
            try
            {
                Users user = await _mangoDbService.UserModel
                    .Find(u => u.Name == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return null;
                }

                return _mapper.Map<UsersDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user");
                throw;
            }
        }

        public async Task<UsersDto> Register(UsersDto newUser)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user");
                throw;
            }
        }
    }
}
