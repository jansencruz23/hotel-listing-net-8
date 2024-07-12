using HotelListing.Application.Constants;
using HotelListing.Application.Contracts.Identity;
using HotelListing.Application.Models.Identity;
using HotelListing.Identity.Models;
using MediatR.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest request)
        {
            _logger.LogInformation($"Login attempt for '{request.Username}'");

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                _logger.LogError($"User '{request.Username}' is not found.");
                throw new Exception($"User with username '{request.Username}' not found.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                _logger.LogError($"Invalid credentials for user '{request.Username}'.");
                throw new Exception($"Password for user '{request.Username}' is incorrect.");
            }

            var jwtSecurityToken = await GenerateJwtToken(user);
            var response = new AuthenticationResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName
            };

            _logger.LogInformation($"User '{request.Username}' logged in successfully.");
            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            _logger.LogInformation($"Registration attempt for '{request.Username}'.");

            var existingUser = await _userManager.FindByNameAsync(request.Username);
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                _logger.LogError($"User with username '{request.Username}' is already existing.");
                throw new Exception($"Username '{request.Username}' already exists.");
            }

            if (existingEmail != null)
            {
                _logger.LogError($"Email '{request.Email}' is already existing.");
                throw new Exception($"Email '{request.Email}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                _logger.LogError($"An error in the registration occurred: {result.Errors}");
                throw new Exception($"{result.Errors}");
            }

            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogInformation($"Registered '{request.Username}' successfully.");
            return new RegistrationResponse { UserId = user.Id };
        }

        private async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var jwtKey = Environment.GetEnvironmentVariable("HOTELLISTING_JWT_KEY");
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
    }
}
