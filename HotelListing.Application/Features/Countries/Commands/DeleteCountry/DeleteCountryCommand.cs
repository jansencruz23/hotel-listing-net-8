using HotelListing.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Commands.DeleteCountry
{
    public record DeleteCountryCommand(int Id) : IRequest<BaseCommandResponse>;
}
