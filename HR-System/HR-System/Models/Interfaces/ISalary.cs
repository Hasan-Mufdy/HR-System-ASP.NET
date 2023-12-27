namespace HR_System.Models.Interfaces
{
    public interface ISalary
    {
        Task<Salary> PostSalary(Salary salary);
        Task<List<Salary>> GetAllSalaries();

    }
}
