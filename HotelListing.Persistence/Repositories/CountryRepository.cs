using HotelListing.Application.Contracts.Persistence;
using HotelListing.Domain.Models;
using HotelListing.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(HotelListingDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
