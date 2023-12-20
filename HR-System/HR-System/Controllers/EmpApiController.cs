using HR_System.Models;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpApiController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmpApiController(IEmployee employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _employee.GetAllEmployees();
        }
    }
}
