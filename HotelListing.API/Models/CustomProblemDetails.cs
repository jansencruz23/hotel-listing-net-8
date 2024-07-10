using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; set; } = new();
    }
}
