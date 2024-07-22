using HotelListing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(q => q.Version)
                .IsConcurrencyToken(); // For other DB

            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "So Good Hotel",
                    Rating = 5,
                    Address = "Pandan, Angeles City",
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "So Bad Hotel",
                    Rating = 1,
                    Address = "Salapungan, Angeles City",
                    CountryId = 1
                }
            );
        }
    }
}
