using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Models;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Services.Base.Responses;

namespace HotelListing.MVC.Contracts
{
    public interface IHotelService
    {
        Task<List<HotelVM>> GetAllHotels();
        Task<HotelVM> GetHotel(int id);
        Task<UpdateHotelVM> GetUpdateHotel(int id);
        Task<Response<int>> CreateHotel(CreateHotelVM hotel);
        Task<Response<int>> UpdateHotel(int id, UpdateHotelVM hotel);
        Task<Response<int>> DeleteHotel(int id);
    }
}
