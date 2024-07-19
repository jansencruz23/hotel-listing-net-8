using HotelListing.Application.DTOs.Hotel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Queries.GetHotel
{
    public record GetHotelDetailRequest(int Id) : IRequest<HotelDto>;
}
