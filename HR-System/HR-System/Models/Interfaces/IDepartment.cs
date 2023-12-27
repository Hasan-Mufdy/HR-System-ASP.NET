namespace HR_System.Models.Interfaces
{
    public interface IDepartment
    {
        Task<Department> PostDepartment(Department department);
        Task<List<Department>> GetAllDepartments();

    }
}
