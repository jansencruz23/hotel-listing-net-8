using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Country;
using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Features.Countries.Requests.Queries;
using HotelListing.Application.Features.Hotels.Handlers.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Handlers.Queries
{
    public class GetCountryDetailRequestHandler(
        IUnitOfWork _unitOfWork,
        ILogger<GetCountryDetailRequestHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<GetCountryDetailRequest, CountryDto>
    {
        public async Task<CountryDto> Handle(GetCountryDetailRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(GetCountryDetailRequestHandler)}");

            try
            {
                var country = await _unitOfWork.CountryRepository.GetCountryWithDetails(request.Id);
                var countryDto = _mapper.Map<CountryDto>(country);

                _logger.LogInformation($"Successfully fetched and mapped {nameof(CountryDto)}");
                return countryDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching and mapping");
                throw;
            }
        }
    }
}
