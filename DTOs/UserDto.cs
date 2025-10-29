using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.DTOs
{
    public class UserDto
    {

        public Guid Id { get; set; }


        public string Name { get; set; }


        public string Email { get; set; }


        public string Password { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime LastUpdateAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
        public string? Token { get; set; } 

    }
}
