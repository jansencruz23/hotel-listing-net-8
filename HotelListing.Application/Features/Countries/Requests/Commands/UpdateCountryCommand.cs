using HotelListing.Application.DTOs.Country;
using HotelListing.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Requests.Commands
{
    public record UpdateCountryCommand(int Id, UpdateCountryDto CountryDto) : IRequest<BaseCommandResponse>;
}
