using FinanceWalletIOAPI.DTOs;
using FinanceWalletIOAPI.DTOs.Base;
using FinanceWalletIOAPI.IRepositories;
using FinanceWalletIOAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceWalletIOAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthRepository(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }


        public async Task<ResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return new ResponseDto
                {
                    Status = false,
                    Msg = "This emial already associate with an other email."
                };

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return new ResponseDto
                {
                    Status = false,
                    Msg = string.Join(", ", result.Errors.Select(e => e.Description))
                };

            return new ResponseDto
            {
                Status = true,
                Msg = "User registered succefully",
                Data = new { id = $"{user.Id}", idDataType = $"{user.Id.GetType()}" } // for testing
            };
        }


        public async Task<ResponseDto> LoginAsync(LoginDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser == null)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"{dto.Email} not exist!"
                };

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, dto.Password, false);
            if (!result.Succeeded)
                return new ResponseDto
                {
                    Status = false,
                    Msg = $"Wrong email or password!"
                };

            string token = GenerateJWToken(existingUser);
            return new ResponseDto
            {
                Status = true,
                Msg = token,
                Data = new { id = $"{existingUser.Id}", idDataType = $"{existingUser.Id.GetType()}" } // for testing
            };
        }


        public string GenerateJWToken(User user)
        {
            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Name", user.Name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); // Create a symmetric security key using the secret key from configuration.
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Create signing credentials using the secret key and HMAC SHA256 algorithm.

            var token = new JwtSecurityToken // Create a new JWT token with the specified claims and signing credentials.
            (
                issuer: _config["Jwt:Issuer"], // Set the issuer of the token.
                audience: _config["Jwt:Audience"], // Set the audience of the token.
                claims: Claims, // Add claims to the token.
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpirationMinutes"]!)), // Set token expiration time.
                signingCredentials: credentials // Use the signing credentials to sign the token.
            );
            return new JwtSecurityTokenHandler().WriteToken(token); // Convert the token to a string.
        }


        public async Task<ResponseDto> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new ResponseDto
            {
                Status = true,
                Msg = "User logged out successfully"
            };
        }
    }
}
