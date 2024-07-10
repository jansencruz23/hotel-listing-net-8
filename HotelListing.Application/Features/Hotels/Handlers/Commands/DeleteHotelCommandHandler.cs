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
    public class DeleteHotelCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<DeleteHotelCommandHandler> _logger
        ) : IRequestHandler<DeleteHotelCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(DeleteHotelCommandHandler)}");

            var response = new BaseCommandResponse();
            var existing = await _unitOfWork.HotelRepository.Exists(request.Id);

            if (!existing)
            {
                response.Success = false;
                response.Message = $"{nameof(Hotel)} with id: {request.Id} is not existing";

                throw new NotFoundException(nameof(Hotel), request.Id);
            }

            await _unitOfWork.HotelRepository.Delete(request.Id);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Message = "Deletion is successful";
            response.Id = request.Id;

            _logger.LogInformation($"Succesful delete in {nameof(UpdateHotelCommandHandler)}");
            return response;
        }
    }
}
