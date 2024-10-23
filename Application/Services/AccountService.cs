using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    internal sealed class AccountService(UserManager<ApplicationUser> _userManager, IMapper _mapper, ITokenService _tokenService) : IAccountService
    {
        public async Task<UserDTO> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (passwordValid)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Token = await _tokenService.CreateToken(user);
                return userDto;
            }

            return null;
        }

        public async Task<UserDTO> Register(RegisterDTO registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            if (!IsValidPassword(registerDto.Password))
            {
                throw new Exception("Password must contain at least one uppercase letter, one number, and one symbol");
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                var isFirstUser = !_userManager.Users.Any();  

                if (isFirstUser)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "User"); 
                }
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Token = await _tokenService.CreateToken(user);
                return userDto;
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to register the user: {errors}");
        }

        private bool IsValidPassword(string password)
        {
            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasDigits = password.Any(char.IsDigit);
            var hasSymbols = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasDigits && hasSymbols;
        }

    }
}
