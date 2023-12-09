using System.ComponentModel.DataAnnotations.Schema;

namespace HR_System.Models
{
    public class Salary
    {
        public int Id { get; set; }

        //public int EmployeeId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public DateTime EffectiveDate { get; set; }

        //public Employee Employee { get; set; }
    }
}
