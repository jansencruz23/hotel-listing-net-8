using HotelListing.Application.DTOs.Country;
using HotelListing.Application.Features.Countries.Commands.CreateCountry;
using HotelListing.Application.Features.Countries.Commands.DeleteCountry;
using HotelListing.Application.Features.Countries.Commands.UpdateCountry;
using HotelListing.Application.Features.Countries.Queries.GetCountry;
using HotelListing.Application.Features.Countries.Queries.GetCountryList;
using HotelListing.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController(
        IMediator _mediator
        ) : ControllerBase
    {

        // GET: api/<CountryController>
        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> Get()
        {
            var response = await _mediator.Send(new GetCountryListRequest());
            return Ok(response);
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> Get(int id)
        {
            var response = await _mediator.Send(new GetCountryDetailRequest(id));
            return Ok(response);
        }

        // POST api/<CountryController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCountryDto dto)
        {
            var response = await _mediator.Send(new CreateCountryCommand(dto));
            return Ok(response);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateCountryDto dto)
        {
            var response = await _mediator.Send(new UpdateCountryCommand(id, dto));
            return Ok(response);
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCountryCommand(id));
            return Ok(response);
        }
    }
}
