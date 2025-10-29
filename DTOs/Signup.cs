using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.DTOs
{
    public class Signup
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
