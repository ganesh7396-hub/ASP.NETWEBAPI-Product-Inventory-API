using Microsoft.AspNetCore.Mvc;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Service.Interfaces;

namespace Product_Inventory_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] Signup dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userService.SignupAsync(dto);
                return Ok(new { message = "User registered successfully.", user });
            

          
        }




        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] Signin dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.SigninAsync(dto);
            if (user == null) return Unauthorized(new { error = "Invalid email or password." });

            return Ok(new {  user });
        }
    }
}
