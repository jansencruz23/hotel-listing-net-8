using HotelListing.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<AuthenticationResponse> Login(AuthenticationRequest request);
    }
}
