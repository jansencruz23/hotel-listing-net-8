using AutoMapper;
using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Models.Pagination;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Base.Responses;
using Microsoft.Build.Logging;
using X.PagedList;

namespace HotelListing.MVC.Services
{
    public class HotelService : BaseHttpService, IHotelService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;

        public HotelService(
            IClient client, 
            ILocalStorageService localStorage, 
            IMapper mapper) : base(client, localStorage)
        {
            _localStorage = localStorage;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateHotel(CreateHotelVM hotel)
        {
            try
            {
                var response = new Response<int>();
                var hotelDto = _mapper.Map<CreateHotelDto>(hotel);

                AddBearerToken();
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

        public async Task<Response<int>> DeleteHotel(int id)
        {
            try
            {
                var response = new Response<int>();

                AddBearerToken();
                var apiResponse = await _client.HotelDELETEAsync(id);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Data = id;
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<IPagedList<HotelVM>> GetAllHotels(RequestParams requestParams)
        {
            var response = await _client.HotelGETAsync(requestParams.PageNumber, requestParams.PageSize);
            var hotels = _mapper.Map<List<HotelVM>>(response.Data);
            var totalCount = response.TotalCount;

            return new StaticPagedList<HotelVM>(hotels, requestParams.PageNumber, requestParams.PageSize, totalCount);
        }

        public async Task<HotelVM> GetHotel(int id)
        {
            var hotel = await _client.HotelGET2Async(id);
            return _mapper.Map<HotelVM>(hotel);
        }

        public async Task<UpdateHotelVM> GetUpdateHotel(int id)
        {
            var hotel = await _client.HotelGET2Async(id);
            return _mapper.Map<UpdateHotelVM>(hotel);
        }

        public async Task<Response<int>> UpdateHotel(int id, UpdateHotelVM hotel)
        {
            try
            {
                var response = new Response<int>();
                var hotelDto = _mapper.Map<UpdateHotelDto>(hotel);

                AddBearerToken();
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
