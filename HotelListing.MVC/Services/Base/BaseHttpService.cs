using HotelListing.MVC.Contracts;
using HotelListing.MVC.Services.Base.Responses;
using Microsoft.Build.Evaluation;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HotelListing.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        protected ILocalStorageService _localStorage;

        public BaseHttpService(
            IClient client,
            ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            var defaultErrorMessage = "Something went wrong, please try again.";
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(ex.Response);
            var response = new Response<Guid>
            {
                Message = "Something went wrong, please try again.",
                Errors = new List<string> { errorResponse.Title ?? defaultErrorMessage },
                Success = false
            };

            switch (ex.StatusCode)
            {
                case StatusCodes.Status400BadRequest:
                    response.Message = "Validation errors have occurred.";
                    response.Errors = errorResponse.Errors;
                    return response;

                case StatusCodes.Status404NotFound:
                    response.Message = "The requested item could not be found.";
                    return response;

                case StatusCodes.Status409Conflict:
                    response.Message = "The item you attempted to update was modified by another user.";
                    return response;

                case StatusCodes.Status401Unauthorized: // sample lang
                    response.Message = "You are unauthorized to visit this page";
                    return response;

                default:
                    return response;
            }
        }

        protected void AddBearerToken()
        {
            if (_localStorage.Exists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
            }
        }
    }
}
