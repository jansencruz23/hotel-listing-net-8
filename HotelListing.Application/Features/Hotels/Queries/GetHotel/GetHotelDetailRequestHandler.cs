using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Hotel;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Queries.GetHotel
{
    public class GetHotelDetailRequestHandler(
        IUnitOfWork _unitOfWork,
        ILogger<GetHotelDetailRequestHandler> _logger,
        IMapper _mapper)
        : IRequestHandler<GetHotelDetailRequest, HotelDto>
    {
        public async Task<HotelDto> Handle(GetHotelDetailRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(GetHotelDetailRequestHandler)}");

            var hotel = await _unitOfWork.HotelRepository.GetHotelWithDetails(request.Id);
            var hotelDto = _mapper.Map<HotelDto>(hotel);

            _logger.LogInformation($"Successfully fetched and mapped {nameof(HotelDto)}");
            return hotelDto;
        }
    }
}
