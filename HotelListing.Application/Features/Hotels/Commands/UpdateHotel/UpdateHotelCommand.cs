using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Commands.UpdateHotel
{
    public record UpdateHotelCommand(int Id, UpdateHotelDto HotelDto) : IRequest<BaseCommandResponse>;
}
