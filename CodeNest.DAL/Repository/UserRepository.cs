// ***********************************************************************************************
//
//  (c) Copyright 2023, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: This repository provides functionalities to manage user data, including retrieval,
//  login, and registration of users.
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

        /// <summary>
        /// Retrieves a user by their identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user details if found, otherwise null.</returns>
        public async Task<UsersDto?> GetUserById(string id)
        {
            _logger.LogInformation("GetUserById: Retrieving user by ID.");

            try
            {
                Users existingUser = await _mangoDbService.UserModel
                    .Find(u => u.Id.ToString() == id)
                    .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    _logger.LogWarning("GetUserById: User not found.");
                    return null;
                }

                _logger.LogInformation("GetUserById: Successfully retrieved user.");
                return _mapper.Map<UsersDto>(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserById: An error occurred while retrieving the user.");
                throw;
            }
        }

        /// <summary>
        /// Logs in a user with the provided username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user details if login is successful, otherwise null.</returns>
        public async Task<UsersDto?> Login(string username, string password)
        {
            _logger.LogInformation("Login: Attempting to log in user.");

            try
            {
                Users user = await _mangoDbService.UserModel
                    .Find(u => u.Name == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    _logger.LogWarning("Login: Invalid username or password.");
                    return null;
                }

                _logger.LogInformation("Login: Successfully logged in user.");
                return _mapper.Map<UsersDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login: An error occurred while logging in the user.");
                throw;
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="newUser">The new user details.</param>
        /// <returns>The registered user details if successful, otherwise null.</returns>
        public async Task<UsersDto?> Register(UsersDto newUser)
        {
            _logger.LogInformation("Register: Attempting to register a new user.");

            try
            {
                Users existingUser = await _mangoDbService.UserModel
                    .Find(u => u.Name == newUser.Name)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    _logger.LogWarning("Register: User with the same name already exists.");
                    return null;
                }

                await _mangoDbService.UserModel.InsertOneAsync(_mapper.Map<Users>(newUser));
                _logger.LogInformation("Register: Successfully registered new user.");
                return newUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register: An error occurred while registering the user.");
                throw;
            }
        }
    }
}
