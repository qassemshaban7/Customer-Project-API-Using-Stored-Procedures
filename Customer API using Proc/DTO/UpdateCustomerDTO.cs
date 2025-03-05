using System.ComponentModel.DataAnnotations;

namespace Company.DTO
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Salary { get; set; }
        public string? Password { get; set; }
        public int? DeptId { get; set; }
    }
}
