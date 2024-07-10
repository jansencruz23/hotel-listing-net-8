using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.DTOs.Country
{
    public class CreateCountryDto : ICountryDto
    {
        public string Name { get; set; }
        public string CodeName { get; set; }
    }
}
