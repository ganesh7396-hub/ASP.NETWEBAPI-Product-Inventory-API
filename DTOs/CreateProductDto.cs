using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.DTOs
{
    public class CreateProductDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
