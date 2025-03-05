using System.ComponentModel.DataAnnotations;

namespace Company.DTO
{
    public class CustomerProjectDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
