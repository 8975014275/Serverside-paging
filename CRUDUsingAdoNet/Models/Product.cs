using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDUsingAdoNet.Models
{
    [Table("Product")]//product model class map with product table in database
    public class Product
    {
        [Key]//defines that id is primary key
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string? Name { get; set; }//allow null use"?"
        [Required(ErrorMessage = " company name is required")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "price is required")]
        public int? Price { get; set; }

    }
}
