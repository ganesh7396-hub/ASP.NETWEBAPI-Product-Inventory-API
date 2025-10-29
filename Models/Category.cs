using System.ComponentModel.DataAnnotations;

namespace Product_Inventory_Management_API.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime LastUpdateAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

    }
}
