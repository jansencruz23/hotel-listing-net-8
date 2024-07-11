using AutoMapper;
using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Base.Responses;
using Microsoft.Build.Logging;

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

        public async Task<Response<int>> CreateHotel(CreateHotelVM hotel)
        {
            try
            {
                var response = new Response<int>();
                var hotelDto = _mapper.Map<CreateHotelDto>(hotel);
                var apiResponse = await _client.HotelPOSTAsync(hotelDto);

                if (apiResponse.Success)
                {
                    response.Success = true;
                    response.Data = apiResponse.Id;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.Errors.Add(error);
                    }
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public Task<Response<int>> DeleteHotel(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HotelVM>> GetAllHotels()
        {
            var hotels = await _client.HotelAllAsync();
            return _mapper.Map<List<HotelVM>>(hotels);
        }

        public async Task<HotelVM> GetHotel(int id)
        {
            var hotel = await _client.HotelGETAsync(id);
            return _mapper.Map<HotelVM>(hotel);
        }

        public async Task<UpdateHotelVM> GetUpdateHotel(int id)
        {
            var hotel = await _client.HotelGETAsync(id);
            return _mapper.Map<UpdateHotelVM>(hotel);
        }

        public async Task<Response<int>> UpdateHotel(int id, UpdateHotelVM hotel)
        {
            try
            {
                var response = new Response<int>();
                var hotelDto = _mapper.Map<UpdateHotelDto>(hotel);
                var apiResponse = await _client.HotelPUTAsync(id, hotelDto);

                if (apiResponse.Success)
                {
                    response.Success = true;
                    response.Data = apiResponse.Id;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.Errors.Add(error);
                    }
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
