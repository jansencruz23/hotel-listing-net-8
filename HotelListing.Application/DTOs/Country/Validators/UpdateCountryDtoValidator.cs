using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Country.Validators
{
    public class UpdateCountryDtoValidator : AbstractValidator<UpdateCountryDto>
    {
        public UpdateCountryDtoValidator()
        {
            Include(new ICountryDtoValidator());

            RuleFor(q => q.Id)
                .NotNull().WithMessage("{PropertyName} must not be empty.")
                .GreaterThan(0).WithMessage("{PropertName} must be at least 1.");
        }
    }
}
