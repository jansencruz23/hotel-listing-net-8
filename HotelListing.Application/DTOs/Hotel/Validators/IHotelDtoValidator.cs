using FluentValidation;
using HotelListing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Hotel.Validators
{
    public class IHotelDtoValidator : AbstractValidator<IHotelDto>
    {
        public IHotelDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(q => q.Name)
                .NotNull().WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(q => q.Address)
                .NotNull().WithMessage("{PropertyName} must not be empty.");

            RuleFor(q => q.Rating)
                .NotNull().WithMessage("{PropertyName} must not be empty.")
                .GreaterThan(0).WithMessage("{PropertyName} must not greater than {ComparisonValue}.")
                .LessThanOrEqualTo(5).WithMessage("{PropertyName} must be less than or equal {ComparisonValue}.");

            RuleFor(q => q.CountryId)
                .GreaterThan(0).WithMessage("{PropertyName} must not be empty.")
                .MustAsync(async (id, token) =>
                {
                    var existing = await unitOfWork.CountryRepository.Exists(id);
                    return existing;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
