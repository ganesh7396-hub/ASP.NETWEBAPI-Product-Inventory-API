using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;



namespace Product_Inventory_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;   
        }


        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto dto)
        {
            try
            {

                var product = await _productService.CreateProductAsync(dto);
                return Ok(product);

            }
            catch (Exception ex) { 
            
            return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update")]


        public async Task<IActionResult> UpdateProduct(Guid ProductId, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(ProductId, dto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> DeleteProduct(Guid ProductId)
        {
            var result =await _productService.DeleteProductAsync(ProductId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetProduct(Guid ProductId)
        {

            var result = await _productService.GetProductAsync(ProductId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet()]
        public  async Task<IActionResult> GetAllProducts ()
        {
            var result =await _productService.GetAllProductAsync();
            return Ok(result);
        }



    }
}
