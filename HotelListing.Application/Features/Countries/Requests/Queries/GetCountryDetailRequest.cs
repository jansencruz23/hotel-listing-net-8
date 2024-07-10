using HotelListing.Application.DTOs.Country;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Requests.Queries
{
    public record GetCountryDetailRequest(int Id) : IRequest<CountryDto>;
}
