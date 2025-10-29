namespace Product_Inventory_Management_API.DTOs
{
    public class ProductDto
    {


        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } 

        public DateTime CreateAt { get; set; }

        public DateTime LastUpdateAt { get; set; }

        public bool IsDeleted { get; set; }

    }
}
