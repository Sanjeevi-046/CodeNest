// ***********************************************************************************************
//
//  (c) Copyright 2024, Computer Task Group, Inc. (CTG)
//
//  This software is licensed under a commercial license agreement. For the full copyright and
//  license information, please contact CTG for more information.
//
//  Description: CodeNest .
//
// ***********************************************************************************************

using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;

namespace CodeNest.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="logger">The logger instance.</param>
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UsersDto> GetUserByNameIdentifier(string id)
        {
            UsersDto result = await _userRepository.GetUserByNameIdentifier(id);
            return result;
        }
        /// <summary>
        /// Gets the user detail by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user details.</returns>
        public async Task<UsersDto> GetUserById(string id)
        {
            _logger.LogInformation("GetUserById: Retrieving user with ID.");

            try
            {
                UsersDto result = await _userRepository.GetUserById(id);
                _logger.LogInformation("GetUserById: Successfully retrieved user.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserById: An error occurred while retrieving user.");
                throw;
            }
        }

        /// <summary>
        /// Checks if the user exists in the database and returns the user details.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The user details if the user exists.</returns>
        public async Task<UsersDto> Login(string username, string password)
        {
            _logger.LogInformation("Login: Attempting login for user.");

            try
            {
                UsersDto user = await _userRepository.Login(username, password);
                _logger.LogInformation("Login: Successfully logged in user.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login: An error occurred while logging in user.");
                throw;
            }
        }

        /// <summary>
        /// Registers a new user in the database.
        /// </summary>
        /// <param name="newUser">The new user details.</param>
        /// <returns>The registered user details.</returns>
        public async Task<UsersDto?> Register(UsersDto newUser)
        {
            _logger.LogInformation("Register: Attempting to register user.");

            try
            {
                UsersDto user = await _userRepository.Register(newUser);
                _logger.LogInformation("Register: Successfully registered user.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register: An error occurred while registering user.");
                throw;
            }
        }
    }
}
