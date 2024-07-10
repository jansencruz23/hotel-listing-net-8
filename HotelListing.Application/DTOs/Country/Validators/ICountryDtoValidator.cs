using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Country.Validators
{
    public class ICountryDtoValidator : AbstractValidator<ICountryDto>
    {
        public ICountryDtoValidator()
        {
            RuleFor(q => q.Name)
                .NotNull().WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(q => q.CodeName)
                .NotNull().WithMessage("{PropertyName} must not be empty.")
                .MaximumLength(3).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
        }
    }
}
