using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using HR_System.Data;
using HR_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Models.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public EmployeeService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public async Task<double> SalaryAvg()
        {
            var empNumber = await _context.Employees
                .Where(e => e.Salary != null)
                .CountAsync();
            
            var salaryList = await _context.Employees
                .Include(e => e.Salary)
                .ToListAsync();

            //if (empNumber == 0)
            //{
            //    return 0.0;
            //}
            var totalSalary = await _context.Employees
                .Where(e => e.Salary != null)
                .SumAsync(e => e.Salary.Amount);

            if (empNumber == 1)
            {
                return totalSalary;
            }
            //var totalSalary = await _context.Employees
            //    .Where(e => e.Salary != null)
            //    .SumAsync(e => e.Salary.Amount);
            if(empNumber > 0)
            {
                double averageSalary = totalSalary / empNumber;
                return averageSalary;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> Count()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> CountHR()
        {
            return await _context.Employees
                .Where(e => e.Position != null && e.Position.Name == "HR")
                .CountAsync();
        }

        public async Task<int> CountManager()
        {
            return await _context.Employees
                .Where(e => e.Position != null && e.Position.Name == "Manager")
                .CountAsync();
        }

        public async Task<int> CountIT()
        {
            return await _context.Employees
                .Where(e => e.Position != null && e.Position.Name == "IT")
                .CountAsync();
        }
        public async Task<int> CountTeamLeader()
        {
            return await _context.Employees
                .Where(e => e.Position != null && e.Position.Name == "Team Leader")
                .CountAsync();
        }

        ///////////////////////////////////////////////////////

        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Salary)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Where(e => e.Id == id).SingleOrDefaultAsync();
            return employee;
        }

        public async Task<Employee> PostEmployee(Employee employee, IFormFile file)
        {
            var URL = await UploudFile(file);

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
                Department = employee.Department,
                ImageUrl = URL
            };
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
            return emp;
        }

        public async Task UpdateEmployee(int id, Employee newEmployee, IFormFile file)
        {
            var existingEmployee = await _context.Employees
                //.Include(e => e.Salary)
                .SingleOrDefaultAsync(e => e.Id == id);
            //if (existingEmployee != null)
            //{
            //    return null;
            //}
            existingEmployee.Name = newEmployee.Name;
            existingEmployee.DateOfBirth = newEmployee.DateOfBirth;
            existingEmployee.Number = newEmployee.Number;
            existingEmployee.Email = newEmployee.Email;

            existingEmployee.SalaryId = newEmployee.SalaryId;
            existingEmployee.PositionId = newEmployee.PositionId;
            existingEmployee.DepartmentId = newEmployee.DepartmentId;

            if (file != null)
            {
                var ImageUrl = await UploudFile(file);
                existingEmployee.ImageUrl = ImageUrl;
            }
            else
            {
                existingEmployee.ImageUrl = existingEmployee.ImageUrl;
            }



            // salary:
            //if(existingEmployee.Salary != null)
            //{
            //    existingEmployee.Salary.Amount = newEmployee.Salary.Amount;
            //    existingEmployee.Salary.Currency = newEmployee.Salary.Currency;
            //    existingEmployee.Salary.EffectiveDate = newEmployee.Salary.EffectiveDate;
            //}

            _context.Update(existingEmployee);
            await _context.SaveChangesAsync();
        }
        public async Task<string> UploudFile(IFormFile file)
        {
            var URL = "https://hrsystem.blob.core.windows.net/images/noimage.png";
            if (file != null)
            {
                BlobContainerClient blobContainerClient =
                    new BlobContainerClient(_configuration.GetConnectionString("StorageAccount"), "images");

                await blobContainerClient.CreateIfNotExistsAsync();

                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

                using var fileStream = file.OpenReadStream();

                BlobUploadOptions blobUploadOptions = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
                };

                if (!blobClient.Exists())
                {
                    await blobClient.UploadAsync(fileStream, blobUploadOptions);
                }
                URL = blobClient.Uri.ToString();
            }
            return URL;
        }
    }
}
