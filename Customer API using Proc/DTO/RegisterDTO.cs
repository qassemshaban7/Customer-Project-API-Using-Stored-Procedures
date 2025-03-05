using System.ComponentModel.DataAnnotations;

namespace University.DTO
{

        public class RegisterDTO
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public decimal Salary { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public int DeptId { get; set; }
        }
}
