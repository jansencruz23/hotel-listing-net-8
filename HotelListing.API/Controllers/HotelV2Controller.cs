using Asp.Versioning;
using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Features.Hotels.Queries.GetHotelList;
using HotelListing.Application.Models.Pagination;
using HotelListing.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/2.0/hotel")]
    [ApiController]
    public class HotelV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelV2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<HotelController>
        [HttpGet]
        public async Task<ActionResult<PagedQueryResponse<HotelDto>>> Get([FromQuery] RequestParams requestParams = null)
        {
            var response = await _mediator.Send(new GetHotelListRequest(requestParams));
            return Ok(response);
        }
    }
}
