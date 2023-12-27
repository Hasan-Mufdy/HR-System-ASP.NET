using HR_System.Models;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalary _salary;
        public SalaryController(ISalary salary)
        {
            _salary = salary;
        }
        public async Task<IActionResult> Index()
        {
            var salaries = await _salary.GetAllSalaries();
            return View(salaries);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Salary salary)
        {
            if (!ModelState.IsValid)
            {
                return View(salary);
            }

            await _salary.PostSalary(salary);
            return RedirectToAction(nameof(Index));
        }
    }
}
