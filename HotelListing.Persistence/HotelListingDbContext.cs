using HotelListing.Application.Abstractions.Identity;
using HotelListing.Domain.Models;
using HotelListing.Domain.Models.Common;
using HotelListing.Persistence.Configurations.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence
{
    public class HotelListingDbContext : DbContext
    {
        private readonly IUserService _userService;

        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            //modelBuilder.ApplyConfiguration(new BaseDomainEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModified = DateTime.Now;
                entry.Entity.LastModifiedBy = _userService.UserId;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }

                entry.Entity.Version = Guid.NewGuid();
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
