using AutoMapper;
using HotelListing.MVC.Constants;
using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Identity;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Base.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelListing.MVC.Services.Identity
{
    public class ClientAuthenticationService : BaseHttpService, IClientAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public ClientAuthenticationService(
            IClient client, 
            ILocalStorageService localStorage,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) 
            : base(client, localStorage)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<Response<string>> Login(LoginVM loginVm)
        {
            var response = new Response<string>();
            response.Success = false;

            try
            {
                var authRequest = _mapper.Map<AuthenticationRequest>(loginVm);
                var authResponse = await _client.LoginAsync(authRequest);

                if (!string.IsNullOrEmpty(authResponse.Token))
                {
                    var tokenContent = _tokenHandler.ReadJwtToken(authResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue("token", authResponse.Token);

                    response.Success = true;
                    response.Data = user.Claims.FirstOrDefault(q => q.Type == CustomClaimTypes.Uid)?.Value;

                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<Response<string>> Register(RegisterVM registrationVm)
        {
            var response = new Response<string>();
            response.Success = false;

            try
            {
                if (!registrationVm.Password.Equals(registrationVm.ConfirmPassword))
                {
                    return response;
                }

                var registrationRequest = _mapper.Map<RegistrationRequest>(registrationVm);
                var registrationResponse = await _client.RegisterAsync(registrationRequest);

                if (!string.IsNullOrEmpty(registrationResponse.UserId))
                {
                    var login = new LoginVM { Username = registrationVm.Username, Password = registrationVm.Password };
                    await Login(login);

                    response.Success = true;
                    response.Data = registrationResponse.UserId;

                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            //claims.Add(new Claim("iddd", tokenContent.Claims.FirstOrDefault(q => q.Type == CustomClaimTypes.Uid).Value));

            return claims;
        }
    }
}
