using HR_System.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static HR_System.Controllers.AuthController;

namespace HR_System.Models.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUserDto data, List<string> roles, ModelStateDictionary modelState);

        public Task<UserDto> Authenticate(string username, string password);

        public Task<UserDto> GetUser(string username);
        public Task Logout();
    }
}
