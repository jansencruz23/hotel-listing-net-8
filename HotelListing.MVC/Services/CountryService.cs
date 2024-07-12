using AutoMapper;
using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models;
using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Services.Base;
using HotelListing.MVC.Services.Base.Responses;
using Newtonsoft.Json;

namespace HotelListing.MVC.Services
{
    public class CountryService : BaseHttpService, ICountryService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;

        public CountryService(
            IClient client,
            ILocalStorageService localStorage,
            IMapper mapper) : base(client, localStorage)
        {
            _localStorage = localStorage;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateCountry(CreateCountryVM country)
        {
            try
            {
                var response = new Response<int>();
                var createCountryDto = _mapper.Map<CreateCountryDto>(country);

                AddBearerToken();
                var apiResponse = await _client.CountryPOSTAsync(createCountryDto);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
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

        public async Task<Response<int>> DeleteCountry(int id)
        {
            try
            {
                var response = new Response<int>();

                AddBearerToken();
                var apiResponse = await _client.CountryDELETEAsync(id);

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

        public async Task<List<CountryVM>> GetAllCountries()
        {
            AddBearerToken();
            var countriesDto = await _client.CountryAllAsync();
            return _mapper.Map<List<CountryVM>>(countriesDto);
        }

        public async Task<CountryVM> GetCountry(int id)
        {
            AddBearerToken();
            var countryDto = await _client.CountryGETAsync(id);
            return _mapper.Map<CountryVM>(countryDto);
        }

        public async Task<UpdateCountryVM> GetUpdateCountry(int id)
        {
            AddBearerToken();
            var countryDto = await _client.CountryGETAsync(id);
            return _mapper.Map<UpdateCountryVM>(countryDto);
        }

        public async Task<Response<int>> UpdateCountry(int id, UpdateCountryVM country)
        {
            try
            {
                var response = new Response<int>();
                var updateCountryDto = _mapper.Map<UpdateCountryDto>(country);

                AddBearerToken();
                var apiResponse = await _client.CountryPUTAsync(id, updateCountryDto);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
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
