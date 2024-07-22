using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Country.Validators;
using HotelListing.Application.DTOs.Hotel.Validators;
using HotelListing.Application.Exceptions;
using HotelListing.Application.Responses;
using HotelListing.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<UpdateCountryCommandHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<UpdateCountryCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(UpdateCountryCommandHandler)}");

            var response = new BaseCommandResponse();
            var validator = new UpdateCountryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CountryDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Updation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                _logger.LogError("Validation Error");
                throw new ValidationException(validationResult);
            }

            var existing = await _unitOfWork.HotelRepository.Get(request.Id);

            if (existing == null)
            {
                response.Success = false;
                response.Message = $"{nameof(Country)} with id: {request.Id} is not existing";

                throw new NotFoundException(nameof(Country), request.Id);
            }

            var country = _mapper.Map<Country>(request.CountryDto);

            try
            {
                _unitOfWork.CountryRepository.Update(country);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error. {ex.Message}");
                throw new ConcurrencyException("The entity you attempted to update was modified by another user.", ex);
            }

            response.Success = true;
            response.Message = "Updation is successful";
            response.Id = request.Id;

            _logger.LogInformation($"Succesful update in {nameof(UpdateCountryCommandHandler)}");
            return response;
        }
    }
}
