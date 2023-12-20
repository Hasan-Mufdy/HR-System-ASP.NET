namespace HR_System.Models.Interfaces
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> PostEmployee(Employee employee);
        Task UpdateEmployee(int id, Employee employee);
        Task DeleteEmployee(int id);
        Task<int> Count();
    }
}
