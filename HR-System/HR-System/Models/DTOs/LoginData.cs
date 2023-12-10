using System.ComponentModel.DataAnnotations;

namespace HR_System.Models.DTOs
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
