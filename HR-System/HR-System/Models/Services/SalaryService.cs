using HR_System.Data;
using HR_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_System.Models.Services
{
    public class SalaryService : ISalary
    {
        private readonly AppDbContext _context;

        public SalaryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Salary> PostSalary(Salary salary)
        {
            var sal = new Salary()
            {
                Id = salary.Id,
                Amount = salary.Amount,
                Currency = salary.Currency,
                EffectiveDate = salary.EffectiveDate,
            };
            await _context.AddAsync(sal);
            await _context.SaveChangesAsync();
            return sal;
        }
    }
}
