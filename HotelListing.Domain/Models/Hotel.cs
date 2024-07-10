using HotelListing.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models
{
    public class Hotel : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
