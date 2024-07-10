using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Hotel.Validators;
using HotelListing.Application.Exceptions;
using HotelListing.Application.Features.Hotels.Requests.Commands;
using HotelListing.Application.Responses;
using HotelListing.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Handlers.Commands
{
    public class UpdateHotelCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<UpdateHotelCommandHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<UpdateHotelCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(UpdateHotelCommandHandler)}");

            var response = new BaseCommandResponse();
            var validator = new UpdateHotelDtoValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.HotelDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Updation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                _logger.LogError("Validation Error");
                throw new ValidationException(validationResult);
            }

            var hotel = await _unitOfWork.HotelRepository.Get(request.Id);

            if (hotel == null)
            {
                response.Success = false;
                response.Message = $"{nameof(Hotel)} with id: {request.Id} is not existing";

                throw new NotFoundException(nameof(Hotel), request.Id);
            }

            _mapper.Map(request.HotelDto, hotel);
            _unitOfWork.HotelRepository.Update(hotel);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Message = "Updation is successful";
            response.Id = request.Id;

            _logger.LogInformation($"Succesful update in {nameof(UpdateHotelCommandHandler)}");
            return response;
        }
    }
}
