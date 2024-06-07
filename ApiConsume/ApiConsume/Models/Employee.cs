using System.ComponentModel.DataAnnotations;

namespace ApiConsume.Models
{
    public class Employee
    {
        public int EmpID { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public string EmpCode { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
