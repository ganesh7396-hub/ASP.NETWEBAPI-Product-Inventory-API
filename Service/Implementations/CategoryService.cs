using Microsoft.EntityFrameworkCore;
using Product_Inventory_Management_API.Data;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Service.Interfaces;
using System;

namespace Product_Inventory_Management_API.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            if (await _context.Categories.AnyAsync(u => u.CategoryName == dto.CategoryName))
            {
                throw new Exception("CategoryName already exists");
            }

            var category = new Category
            {
                CategoryName = dto.CategoryName,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return MapToDto(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.CategoryName = dto.CategoryName;
            await _context.SaveChangesAsync();

            return MapToDto(category);
        }


        public async Task<CategoryDto> GetCategoryDtoAsync(Guid categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            return MapToDto(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(c => MapToDto(c));
        }


        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.IsDeleted = true;
            category.LastUpdateAt = DateTime.Now;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }

        private CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }
    }
}
