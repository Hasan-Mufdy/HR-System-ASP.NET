using HR_System.Data;
using HR_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Models.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> PostDepartment(Department department)
        {
            var dep = new Department()
            {
                Id = department.Id,
                Name = department.Name
            };
            await _context.AddAsync(dep);
            await _context.SaveChangesAsync();
            return dep;
        }
    }
}
