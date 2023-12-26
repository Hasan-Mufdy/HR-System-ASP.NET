using HR_System.Models;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;
        private readonly IConfiguration _configuration;
        public EmployeeController(IEmployee employee, IConfiguration configuration)
        {
            _employee = employee;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string searchTerm)
        {
            List<Employee> employees = await _employee.GetAllEmployees();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                employees = employees.Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Employee employee, IFormFile file)
        {
            ModelState.Remove("file");

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await _employee.PostEmployee(employee, file);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var empDetails = await _employee.GetEmployeeById(id);
            return View(empDetails);
        }

        public async Task<IActionResult> Details(int id)
        {
            var empDetails = await _employee.GetEmployeeById(id);
            return View(empDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile file)
        {
            ModelState.Remove("file");

            //if (!ModelState.IsValid)
            //{
            //    return View(employee);
            //}
            await _employee.UpdateEmployee(id, employee, file);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var empDetails = await _employee.GetEmployeeById(id);
            return View(empDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var empDetails = await _employee.GetEmployeeById(id);
            await _employee.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
