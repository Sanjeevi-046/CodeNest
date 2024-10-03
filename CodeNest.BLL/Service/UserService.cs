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

using CodeNest.BLL.Repositories;
using CodeNest.DTO.Models;

namespace CodeNest.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Initializing instance of <see cref="IUserRepository"/> for accessing the functionality
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Gets the user detail by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>gets the value from <see cref="IUserRepository"/></returns>
        public async Task<UsersDto> GetUserById(string id)
        {
            UsersDto result = await _userRepository.GetUserById(id);
            return result;
        }
        /// <summary>
        /// Checks the User whether already in db or not
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>gets the value from <see cref="IUserRepository"/></returns>
        public async Task<UsersDto> Login(string username, string password)
        {
            UsersDto user = await _userRepository.Login(username, password);
            return user;
        }
        /// <summary>
        /// Adding the User in mongo Database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>gets the value from <see cref="IUserRepository"/> and returns the vale </returns>
        public async Task<UsersDto?> Register(UsersDto newUser)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            UsersDto user = await _userRepository.Register(newUser);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return user;
        }
    }
}
