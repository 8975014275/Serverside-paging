using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDUsingAdoNet.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "student name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = " school name is required")]
        public string? SchoolName { get; set; }
        [Required(ErrorMessage = "percentage is required")]
        public int? Percentage { get; set; }
    }
}
