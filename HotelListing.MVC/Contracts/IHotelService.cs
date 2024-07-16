using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Models;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Services.Base.Responses;
using HotelListing.MVC.Models.Pagination;
using X.PagedList;

namespace HotelListing.MVC.Contracts
{
    public interface IHotelService
    {
        Task<IPagedList<HotelVM>> GetAllHotels(RequestParams requestParams);
        Task<HotelVM> GetHotel(int id);
        Task<UpdateHotelVM> GetUpdateHotel(int id);
        Task<Response<int>> CreateHotel(CreateHotelVM hotel);
        Task<Response<int>> UpdateHotel(int id, UpdateHotelVM hotel);
        Task<Response<int>> DeleteHotel(int id);
    }
}
