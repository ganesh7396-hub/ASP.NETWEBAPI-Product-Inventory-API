using Microsoft.EntityFrameworkCore;
using Product_Inventory_Management_API.Data;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Service.Interfaces;

namespace Product_Inventory_Management_API.Service.Implementations
{
    public class ProductService: IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
        {

            if (await _context.Products.AnyAsync(u => u.Name == dto.Name)) {


                throw new Exception("Product already exist");


            }


            var product = new Product
            {
                ProductId = Guid.NewGuid(),

                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CategoryId = dto.CategoryId

            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return MapToDto(product);

        }

        public async Task<ProductDto> UpdateProductAsync(Guid productId, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return MapToDto(product);
        }






        public async Task<ProductDto> GetProductAsync(Guid productId)
        {
            var product =await _context.Products.FindAsync(productId);
            if (product == null) {

            throw new Exception("Category not found");
            }
            return MapToDto(product);

        }


        public async Task <IEnumerable<ProductDto>> GetAllProductAsync()
        {

            var products =await _context.Products.ToListAsync();
            return products.Select(c => MapToDto(c));

        }


        public async Task<bool> DeleteProductAsync(Guid id)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {

                throw new Exception("Category not found");

            }

            product.IsDeleted = true;
            product.LastUpdateAt = DateTime.UtcNow;
            _context.SaveChanges();
            await _context.SaveChangesAsync();
            return true;
            
        }



        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
               Name = product.Name,
               Description = product.Description,
               Price = product.Price,
               Quantity= product.Quantity,
               CategoryId = product.CategoryId,
                ProductId = product.ProductId,
                CreateAt = product.CreateAt,
                LastUpdateAt = product.LastUpdateAt,
                IsDeleted = product.IsDeleted


            };
        }


    }
}
