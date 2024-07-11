using HotelListing.Identity.Models;
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
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "4a83f348-7aa1-4f09-a654-dbaa9856eda9",
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "admin"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "015dedff-6684-4b70-8586-613f2f3f7323",
                    FirstName = "System",
                    LastName = "User",
                    Email = "user@user.com",
                    NormalizedEmail = "USER@USER.COM",
                    UserName = "user",
                    NormalizedUserName = "user",
                    PasswordHash = hasher.HashPassword(null, "user"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
