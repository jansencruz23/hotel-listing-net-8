using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Features.Hotels.Requests.Queries;
using HotelListing.Application.Responses;
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
        ) : IRequestHandler<GetHotelListRequest, PagedQueryResponse<HotelDto>>
    {
        public async Task<PagedQueryResponse<HotelDto>> Handle(GetHotelListRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(GetHotelListRequestHandler)}");

            try
            {
                var response = new PagedQueryResponse<HotelDto>();
                var hotels = await _unitOfWork.HotelRepository.GetAllHotelsWithDetails(request.RequestParams);
                var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

                response.Data = hotelsDto;
                response.TotalCount = await _unitOfWork.HotelRepository.TotalCount();

                _logger.LogInformation($"Successfully fetched and mapped {nameof(HotelDto)}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching and mapping");
                throw;
            }
        }
    }
}
