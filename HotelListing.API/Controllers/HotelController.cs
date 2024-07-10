using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Features.Hotels.Requests.Commands;
using HotelListing.Application.Features.Hotels.Requests.Queries;
using HotelListing.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<HotelController>
        [HttpGet]
        public async Task<ActionResult<List<HotelDto>>> Get()
        {
            var response = await _mediator.Send(new GetHotelListRequest());
            return Ok(response);
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> Get(int id)
        {
            var response = await _mediator.Send(new GetHotelDetailRequest(id));
            return Ok(response);
        }

        // POST api/<HotelController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHotelDto dto)
        {
            var response = await _mediator.Send(new CreateHotelCommand(dto));
            return Ok(response);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateHotelDto dto)
        {
            var response = await _mediator.Send(new UpdateHotelCommand(id, dto));
            return Ok(response);
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteHotelCommand(id));
            return Ok(response);
        }
    }
}
