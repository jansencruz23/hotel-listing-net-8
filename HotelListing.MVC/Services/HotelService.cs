using AutoMapper;
using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Base.Responses;

namespace HotelListing.MVC.Services
{
    public class HotelService : BaseHttpService, IHotelService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;

        public HotelService(
            IClient client, 
            ILocalStorageService localStorage, 
            IMapper mapper) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
            _mapper = mapper;
        }

        public Task<Response<int>> CreateHotel(CreateHotelVM hotel)
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> DeleteHotel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HotelVM>> GetAllHotels()
        {
            throw new NotImplementedException();
        }

        public Task<HotelVM> GetHotel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> UpdateHotel(int id, UpdateHotelVM country)
        {
            throw new NotImplementedException();
        }
    }
}
