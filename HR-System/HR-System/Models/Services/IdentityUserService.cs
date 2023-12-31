﻿using HR_System.Models.DTOs;
using HR_System.Models.Entities;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static HR_System.Controllers.AuthController;

namespace HR_System.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<AuthUser> _userManager;
        private SignInManager<AuthUser> _signInManager;

        public IdentityUserService(UserManager<AuthUser> manager, SignInManager<AuthUser> signInManager)
        {
            _userManager = manager;
            _signInManager = signInManager;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            if (!user.IsApproved)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            return null;
        }

        //public async Task<UserDto> Authenticate(string username, string password)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

        //    if (result.Succeeded)
        //    {
        //        var user = await _userManager.FindByNameAsync(username);

        //        return new UserDto()
        //        {
        //            Id = user.Id,
        //            Username = user.UserName,
        //            Roles = await _userManager.GetRolesAsync(user)
        //        };
        //    }

        //    return null;
        //}



        public Task<UserDto> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Register(RegisterUserDto data, List<string> roles, ModelStateDictionary modelState)
        {
            var user = new AuthUser()
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                IsApproved = false
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, roles);

                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                     error.Code.Contains("Email") ? nameof(data.Email) :
                     "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
