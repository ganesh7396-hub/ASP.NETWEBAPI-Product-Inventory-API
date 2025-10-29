using Product_Inventory_Management_API.DTOs;

namespace Product_Inventory_Management_API.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> SignupAsync(Signup dto);
        Task<UserDto> SigninAsync(Signin dto);
    }
}
