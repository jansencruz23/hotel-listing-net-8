using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1ba67fe2-2e3f-4db2-8d7c-13707d83f631",
                    UserId = "4a83f348-7aa1-4f09-a654-dbaa9856eda9"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "e0d9b5ec-eca3-48f7-ac37-d893ad604ae0",
                    UserId = "015dedff-6684-4b70-8586-613f2f3f7323"
                }
            );
        }
    }
}
