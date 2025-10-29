using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Product_Inventory_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) {

            _categoryService = categoryService;
        }


        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            var userId = User.FindFirst("Id")?.Value;
            var email = User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            return Ok(new { userId, email });
        }



        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {

            try
            {
                var category = await _categoryService.CreateCategoryAsync(dto);
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);


            }
        }

        [Authorize]
        [HttpPut("update")]

        public async Task<IActionResult> UpdateCategory(Guid CategoryId,[FromBody] UpdateCategoryDto dto)
        {

            try
            {

                var result = await _categoryService.UpdateCategoryAsync(CategoryId, dto);
                return Ok(result);
            }
            catch (Exception ex) { 
               
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpGet("getCategory")]

        public async Task <IActionResult> Getcategory(Guid CategoryId)
        {
            try
            {

                var result = await _categoryService.GetCategoryDtoAsync(CategoryId);

                return Ok(result);
            }
            catch (Exception ex) {


                return NotFound(ex.Message);

            }

        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _categoryService.GetAllCategoriesAsync(); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(categoryId);

                if (result)
                {
                    return NoContent(); 
                }

                return NotFound(); 
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }






    }
}
