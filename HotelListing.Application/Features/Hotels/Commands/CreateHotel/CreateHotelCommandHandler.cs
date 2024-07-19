using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.DTOs.Hotel.Validators;
using HotelListing.Application.Exceptions;
using HotelListing.Application.Responses;
using HotelListing.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<CreateHotelCommandHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<CreateHotelCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(CreateHotelCommandHandler)}");

            var response = new BaseCommandResponse();
            var validator = new CreateHotelDtoValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.HotelDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                _logger.LogError("Validation Error");
                throw new ValidationException(validationResult);
            }

            var hotel = _mapper.Map<Hotel>(request.HotelDto);
            hotel = await _unitOfWork.HotelRepository.Add(hotel);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Message = "Creation is successful";
            response.Id = hotel.Id;

            _logger.LogInformation($"Succesful create in {nameof(CreateHotelCommandHandler)}");
            return response;
        }
    }
}
