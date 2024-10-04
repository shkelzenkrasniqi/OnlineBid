using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<UserDTO> Login(LoginDTO loginDto);
        Task<UserDTO> Register(RegisterDTO registerDto);
    }
}
