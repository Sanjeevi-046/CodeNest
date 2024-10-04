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
using CodeNest.DAL.Repository;
using CodeNest.DTO.Models;
using Microsoft.Extensions.Logging;

namespace CodeNest.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UsersDto> GetUserById(string id)
        {
            try
            {
                UsersDto result = await _userRepository.GetUserById(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID.");
                throw;
            }
        }

        public async Task<UsersDto> Login(string username, string password)
        {
            try
            {
                UsersDto user = await _userRepository.Login(username, password);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");
                throw;
            }
        }

        public async Task<UsersDto?> Register(UsersDto newUser)
        {
            try
            {
                UsersDto? user = await _userRepository.Register(newUser);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                throw;
            }
        }
    }
}
