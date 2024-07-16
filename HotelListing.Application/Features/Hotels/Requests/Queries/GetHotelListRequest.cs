using HotelListing.Application.DTOs.Hotel;
using HotelListing.Application.Models.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Features.Hotels.Requests.Queries
{
    public record GetHotelListRequest(RequestParams RequestParams = null) : IRequest<List<HotelDto>>;
}
