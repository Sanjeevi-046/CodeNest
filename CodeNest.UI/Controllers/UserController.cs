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

using CodeNest.BLL.Service;
using CodeNest.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CodeNest.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Profile(ObjectId userId)
        {
            UsersDto result = await _userService.GetUserById(userId.ToString());
            if (result != null)
            {
                return View(result);
            }
            return View();
        }
    }
}
