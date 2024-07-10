using FluentValidation;
using HotelListing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Hotel.Validators
{
    public class CreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        public CreateHotelDtoValidator(IUnitOfWork unitOfWork)
        {
            Include(new IHotelDtoValidator(unitOfWork));
        }
    }
}
