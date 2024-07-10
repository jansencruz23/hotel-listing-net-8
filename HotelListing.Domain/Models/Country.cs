using HotelListing.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models
{
    public class Country : BaseDomainEntity
    {
        public string Name { get; set; }
        public string CodeName { get; set; }

        public virtual List<Hotel> Hotels { get; set; }
    }
}
