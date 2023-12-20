using HR_System.Models.Entities;
using HR_System.Models.Interfaces;
using HR_System.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmployee _employee;
        private UserManager<AuthUser> _userManager;
        public AdminController(IEmployee employee, UserManager<AuthUser> userManager)
        {
            _employee = employee;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            var adminViewModel = new AdminViewModel
            {
                EmployeesNumber = await _employee.Count(),
            };

            return View(adminViewModel);
        }
        public async Task<IActionResult> PendingUsers()
        {
            var pendingUsers = await _userManager.Users.Where(u => !u.IsApproved).ToListAsync();
            return View(pendingUsers);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.IsApproved = true;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction(nameof(PendingUsers));
        }

        //public async Task<IActionResult> ClaimAdmin()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> ApproveAdmin()
        //{
        //    var user = await _userManager.FindByIdAsync("1");

        //    if (user != null)
        //    {
        //        user.IsApproved = true;
        //        await _userManager.UpdateAsync(user);
        //    }

        //    return RedirectToAction(nameof(PendingUsers));
        //}
    }
}
