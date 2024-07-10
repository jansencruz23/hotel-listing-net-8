using HotelListing.Application.DTOs.Common;
using HotelListing.Application.DTOs.Country;
using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Hotel
{
    public class HotelDto : BaseDto, IHotelDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public CountryDto Country { get; set; }
        public int CountryId { get; set; }
    }
}
