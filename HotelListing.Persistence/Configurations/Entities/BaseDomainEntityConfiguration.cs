using HotelListing.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence.Configurations.Entities
{
    public class BaseDomainEntityConfiguration : IEntityTypeConfiguration<BaseDomainEntity>
    {
        public void Configure(EntityTypeBuilder<BaseDomainEntity> builder)
        {
            //builder.Property(q => q.Version)
            //    .IsRowVersion(); // For SQL only

            builder.Property(q => q.Version)
                .IsConcurrencyToken(); // For other DB
        }
    }
}
