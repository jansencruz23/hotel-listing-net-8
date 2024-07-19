using HotelListing.Application.DTOs.Country;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Queries.GetCountryList
{
    public record GetCountryListRequest() : IRequest<List<CountryDto>>;
}
