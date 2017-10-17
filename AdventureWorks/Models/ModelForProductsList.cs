using System.ComponentModel.DataAnnotations;


namespace AdventureWorks.Models
{
    public class ModelForProductsList
    {
        public int ProductId { get; set; }
        public Photo Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string ProductNumber { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 1)]
        public string Color { get; set; }
        [Required]
        public decimal ListPrice { get; set; }
    }
}
