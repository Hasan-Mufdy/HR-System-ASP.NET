using HR_System.Models.DTOs;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    public enum UserRole
    {
        User,
        HR
    }
    public class AuthController : Controller
    {
        private IUserService userService;
        
        public AuthController(IUserService service)
        {
            userService = service; ;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Signup(RegisterUserDto data)
        {
            if (data.Roles == null || !data.Roles.Any() || data.Roles.Any(role => !Enum.TryParse(role, out UserRole userRole) || !Enum.IsDefined(typeof(UserRole), userRole)))
            {
                ModelState.AddModelError(nameof(data.Roles), "Invalid role selected.");
                return View();
            }

            var user = await userService.Register(data, data.Roles, ModelState);

            if (ModelState.IsValid)
            {
                return Redirect("/");
            }
            return View();
        }


        //[HttpPost]
        //public async Task<ActionResult<UserDto>> Signup(RegisterUserDto data)
        //{
        //    var user = await userService.Register(data, this.ModelState);

        //    if (ModelState.IsValid)
        //    {
        //        return Redirect("/");
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult<UserDto>> Authenticate(LoginData loginData)
        {

            var user = await userService.Authenticate(loginData.Username, loginData.Password);

            if (user == null)
            {
                this.ModelState.AddModelError("InvalidLogin", "Invalid login attempt");

                return RedirectToAction("Index");
            }

            //return RedirectToAction("Index" , "Auth");
            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return Redirect("/");
        }

    }
}
