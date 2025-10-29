using Product_Inventory_Management_API.DTOs;
using System.ComponentModel;

namespace Product_Inventory_Management_API.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<CategoryDto> UpdateCategoryAsync(Guid CategoryId, UpdateCategoryDto dto);

        Task<bool> DeleteCategoryAsync(Guid CategoryId);

        Task<CategoryDto> GetCategoryDtoAsync(Guid CategoryId);

        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

    }
}
