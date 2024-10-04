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

using CodeNest.DTO.Models;

namespace CodeNest.BLL.Repositories
{
    public interface IUserRepository
    {
        Task<UsersDto> GetUserById(string id);
        Task<UsersDto> Login(string username, string password);
        Task<UsersDto> Register(UsersDto newUser);
    }
}
