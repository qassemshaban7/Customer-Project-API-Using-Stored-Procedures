using System.ComponentModel.DataAnnotations;

namespace University.DTO
{
    public class DepartmentName
    {
        [Required]
        public string Name { get; set; }
    }

}
