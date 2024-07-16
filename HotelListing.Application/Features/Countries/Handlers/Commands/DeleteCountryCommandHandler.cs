using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.Exceptions;
using HotelListing.Application.Features.Countries.Requests.Commands;
using HotelListing.Application.Features.Hotels.Handlers.Commands;
using HotelListing.Application.Responses;
using HotelListing.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Handlers.Commands
{
    public class DeleteCountryCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<DeleteCountryCommandHandler> _logger
        ) : IRequestHandler<DeleteCountryCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(DeleteCountryCommandHandler)}");

            if (request.Id < 1)
            {
                _logger.LogError("The request is invalid");
                throw new BadRequestException("The request is invalid.");
            }

            var response = new BaseCommandResponse();
            var existing = await _unitOfWork.CountryRepository.Exists(request.Id);

            if (!existing)
            {
                response.Success = false;
                response.Message = $"{nameof(Country)} with id: {request.Id} is not existing";

                _logger.LogError($"Country not found in {nameof(DeleteCountryCommandHandler)}");
                throw new NotFoundException(nameof(Country), request.Id);
            }

            await _unitOfWork.CountryRepository.Delete(request.Id);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Message = "Deletion is successful";
            response.Id = request.Id;

            _logger.LogInformation($"Succesful delete in {nameof(DeleteCountryCommandHandler)}");
            return response;
        }
    }
}
