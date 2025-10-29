using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product_Inventory_Management_API.Data;
using Product_Inventory_Management_API.DTOs;
using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product_Inventory_Management_API.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<UserDto> SigninAsync(Signin signin)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == signin.Email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, signin.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            var token = GenerateJwtToken(user);

            var userDto = MapToUser(user);
            userDto.Token = token;
            return userDto;
        }



        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]); 

            if (key.Length < 32)
                throw new Exception("JWT key is too short! Use at least 32 characters.");

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("Id", user.Id.ToString()),
        new Claim("Name", user.Name ?? string.Empty)
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }



        public async Task<UserDto> SignupAsync(Signup dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower()))
                throw new Exception("Email is already registered.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                CreateAt = DateTime.UtcNow
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return MapToUser(user);
        }

  

        private static UserDto MapToUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreateAt = DateTime.UtcNow,
                LastUpdateAt = DateTime.UtcNow
            };
        }
    }
}
