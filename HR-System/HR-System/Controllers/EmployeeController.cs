using HR_System.Models;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }
        public async Task<IActionResult> Index()
        {
            var employee = await _employee.GetAllEmployees();
            return View(employee);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await _employee.PostEmployee(employee);
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
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            await _employee.UpdateEmployee(id, employee);
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
