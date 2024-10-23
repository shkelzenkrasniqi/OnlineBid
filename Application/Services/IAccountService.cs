using Domain.DTOs;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<UserDTO> Login(LoginDTO loginDto);
        Task<UserDTO> Register(RegisterDTO registerDto);
    }
}
