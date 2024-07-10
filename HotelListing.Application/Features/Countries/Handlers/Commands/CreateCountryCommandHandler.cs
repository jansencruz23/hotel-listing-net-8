using AutoMapper;
using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Country.Validators;
using HotelListing.Application.DTOs.Hotel.Validators;
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
    public class CreateCountryCommandHandler(
        IUnitOfWork _unitOfWork,
        ILogger<CreateCountryCommandHandler> _logger,
        IMapper _mapper
        ) : IRequestHandler<CreateCountryCommand, BaseCommandResponse>
    {
        public async Task<BaseCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Accessed {nameof(CreateCountryCommandHandler)}");

            var response = new BaseCommandResponse();
            var validator = new CreateCountryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CountryDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                _logger.LogError("Validation Error");
                throw new ValidationException(validationResult);
            }

            var country = _mapper.Map<Country>(request.CountryDto);
            var countryId = await _unitOfWork.CountryRepository.Add(country);
            await _unitOfWork.SaveAsync();

            response.Success = true;
            response.Message = "Creation is successful";
            response.Id = countryId;

            _logger.LogInformation($"Succesful create in {nameof(CreateCountryCommandHandler)}");
            return response;
        }
    }
}
