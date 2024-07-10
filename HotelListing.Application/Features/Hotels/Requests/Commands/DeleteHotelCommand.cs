using HotelListing.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Requests.Commands
{
    public record DeleteHotelCommand(int Id) : IRequest<BaseCommandResponse>;
}
