using HotelListing.Application.Contracts.Identity;
using HotelListing.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
        IAuthenticationService _authenticationService
        ) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
        {
            var response = await _authenticationService.Login(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var response = await _authenticationService.Register(request);
            return Ok(response);
        }
    }
}
