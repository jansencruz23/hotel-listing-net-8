using HotelListing.Application.DTOs.Common;
using HotelListing.Application.DTOs.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Country
{
    public class CountryDto : BaseDto, ICountryDto
    {
        public string Name { get; set; }
        public string CodeName { get; set; }
        public Guid Version { get; set; }

        public List<HotelDto> Hotels { get; set; }
    }
}
