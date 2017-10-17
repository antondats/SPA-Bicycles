using System.ComponentModel.DataAnnotations;


namespace AdventureWorks.Models
{
    public class ModelForProductDetail : ModelForProductsList
    {
        [Required]
        public string Model { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 1)]
        public string Description { get; set; }
        [StringLength(5)]
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        public string Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
