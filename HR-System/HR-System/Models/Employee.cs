using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }

        public int? PositionId { get; set; }

        public int? DepartmentId { get; set; }

        public Salary? Salary { get; set; }
        public int? SalaryId{ get; set; }
        public Position? Position { get; set; }
        public Department? Department { get; set; }
    }
}
