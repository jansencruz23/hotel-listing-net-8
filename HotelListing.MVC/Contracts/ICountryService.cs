using HotelListing.MVC.Models;
using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Services.Base.Responses;

namespace HotelListing.MVC.Contracts
{
    public interface ICountryService
    {
        Task<List<CountryVM>> GetAllCountries();
        Task<CountryVM> GetCountry(int id);
        Task<UpdateCountryVM> GetUpdateCountry(int id);
        Task<Response<int>> CreateCountry(CreateCountryVM country);
        Task<Response<int>> UpdateCountry(int id, UpdateCountryVM country);
        Task<Response<int>> DeleteCountry(int id);
    }
}
