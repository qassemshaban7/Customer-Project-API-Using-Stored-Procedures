using System.ComponentModel.DataAnnotations;

namespace University.DTO
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
