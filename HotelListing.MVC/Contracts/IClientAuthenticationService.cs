using HotelListing.MVC.Models.Identity;
using HotelListing.MVC.Services.Base.Responses;

namespace HotelListing.MVC.Contracts
{
    public interface IClientAuthenticationService
    {
        Task<Response<string>> Login(LoginVM loginVm);
        Task<Response<string>> Register(RegisterVM registrationVm);
        Task Logout();
    }
}
