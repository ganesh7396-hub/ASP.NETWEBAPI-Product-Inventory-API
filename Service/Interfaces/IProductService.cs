using Product_Inventory_Management_API.DTOs;

namespace Product_Inventory_Management_API.Service.Interfaces
{
    public interface IProductService
    {

        Task<ProductDto> CreateProductAsync(CreateProductDto product);
        Task<ProductDto> UpdateProductAsync(Guid ProductId ,UpdateProductDto product);
        Task<bool> DeleteProductAsync(Guid ProductId);

        Task<ProductDto> GetProductAsync(Guid ProductId);
        Task<IEnumerable<ProductDto>> GetAllProductAsync();    

    }
}
