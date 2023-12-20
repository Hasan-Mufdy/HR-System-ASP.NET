using HR_System.Data;
using HR_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Models.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Where(e => e.Id == id).SingleOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> PostEmployee(Employee employee)
        {
            var emp = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                Number = employee.Number,
                Email = employee.Email,
                Position = employee.Position,
                Salary = employee.Salary,
                Department = employee.Department
            };
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task UpdateEmployee(int id, Employee newEmployee)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            //if (existingEmployee != null)
            //{
            //    return null;
            //}
            existingEmployee.Name = newEmployee.Name;
            existingEmployee.DateOfBirth = newEmployee.DateOfBirth;
            existingEmployee.Number = newEmployee.Number;
            existingEmployee.Email = newEmployee.Email;

            _context.Update(existingEmployee);
            await _context.SaveChangesAsync();
        }
    }
}
