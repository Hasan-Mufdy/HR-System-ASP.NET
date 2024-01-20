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
        private readonly InstantiateAdmin _instantiateAdmin;
        public AdminController(IEmployee employee, UserManager<AuthUser> userManager, InstantiateAdmin instantiateAdmin)
        {
            _employee = employee;
            _userManager = userManager;
            _instantiateAdmin = instantiateAdmin;
        }
        public async Task<IActionResult> Index()
        {
            var countHR = await _employee.CountHR();
            var countManager = await _employee.CountManager();
            var countIT = await _employee.CountIT();
            var countTeamLeader = await _employee.CountTeamLeader();

            var avgSalary = await _employee.SalaryAvg();

            var adminViewModel = new AdminViewModel
            {
                EmployeesNumber = await _employee.Count(),
                AvgSalary = avgSalary,
                CountHR = countHR,
                CountIT = countIT,
                CountTeamLeader = countTeamLeader,
                CountManager = countManager
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

        public async Task<IActionResult> ApproveAdmin()
        {
            var user = await _userManager.Users.Where(a => a.UserName == "Admin").SingleOrDefaultAsync();

            if (user != null)
            {
                user.IsApproved = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> ClaimAdmin()
        {
            return View();
        }
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
