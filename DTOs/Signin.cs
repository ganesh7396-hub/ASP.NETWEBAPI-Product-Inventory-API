using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.DTOs
{
    public class Signin
    {
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
