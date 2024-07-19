using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Country;
using HotelListing.Application.DTOs.Hotel;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Queries.GetCountryList
{
    public class GetCountryListRequestHandler(
        IUnitOfWork _unitOfWork,
        ILogger<GetCountryListRequestHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<GetCountryListRequest, List<CountryDto>>
    {
        public async Task<List<CountryDto>> Handle(GetCountryListRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(GetCountryListRequestHandler)}");

            var countries = await _unitOfWork.CountryRepository.GetAllCountriesWithDetails();
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);

            _logger.LogInformation($"Successfully fetched and mapped {nameof(CountryDto)}");
            return countriesDto;
        }
    }
}
