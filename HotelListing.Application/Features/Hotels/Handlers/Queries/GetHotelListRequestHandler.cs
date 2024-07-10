using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Handlers.Queries
{
    public class GetHotelListRequestHandler(
        IUnitOfWork _unitOfWork,
        ILogger<GetHotelListRequestHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<GetHotelListRequest, List<HotelDto>>
    {
        public async Task<List<HotelDto>> Handle(GetHotelListRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(GetHotelListRequestHandler)}");

            try
            {
                var hotels = await _unitOfWork.HotelRepository.GetAllHotelsWithDetails();
                var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

                _logger.LogInformation($"Successfully fetched and mapped {nameof(HotelDto)}");
                return hotelsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching and mapping");
                throw;
            }
        }
    }
}
