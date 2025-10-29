using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.DTOs
{
    public class UpdateCategoryDto
    {

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

    }
}
