using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using atc_backend_app.Data;
using atc_backend_app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace atc_backend_app.Services
{
    public interface IUserService
    {
        Task<ServiceResponse> RegisterAsync(UserDto userDto);
        Task<ServiceResponse> LoginAsync(UserDto userDto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> RegisterAsync(UserDto userDto)
        {
            var response = new ServiceResponse();

            // Check if user already exists
            var existingUser = await _userManager.FindByNameAsync(userDto.Username);
            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            // Create new user
            var user = new IdentityUser
            {
                UserName = userDto.Username,
                Email = userDto.Email // Assuming UserDto has an Email property
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                return response;
            }

            response.Token = CreateToken(user);
            response.Message = "User registered successfully.";
            return response;
        }

        public async Task<ServiceResponse> LoginAsync(UserDto userDto)
        {
            var response = new ServiceResponse();
            var user = await _userManager.FindByNameAsync(userDto.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userDto.Password))
            {
                response.Success = false;
                response.Message = "Invalid credentials.";
                return response;
            }

            response.Token = CreateToken(user);
            response.Message = "Login successful.";
            return response;
        }

        private string CreateToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
