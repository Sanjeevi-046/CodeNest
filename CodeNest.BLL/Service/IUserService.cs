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

using CodeNest.DTO.Models;

namespace CodeNest.BLL.Service
{
    public interface IUserService
    {
        Task<UsersDto> GetUserById(string id);
        Task<UsersDto> Login(string username, string password);
        Task<UsersDto?> Register(UsersDto newUser);
        Task<UsersDto> GetUserByNameIdentifier(string id);
    }
}
