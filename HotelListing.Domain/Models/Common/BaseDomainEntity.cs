using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Domain.Models.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }

        // [Timestamp]
        //public byte[] Version { get; set; } // SQL only

        //[ConcurrencyCheck]
        public Guid Version { get; set; } // For other DB
    }
}
