using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDUsingAdoNet.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "employee name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = " company name is required")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public int? Salary { get; set; }
    }
}
