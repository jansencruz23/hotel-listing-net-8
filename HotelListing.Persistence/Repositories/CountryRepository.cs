using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.DTOs.Country;
using HotelListing.Domain.Models;
using HotelListing.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListingDbContext _dbContext;

        public CountryRepository(HotelListingDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetAllCountriesWithDetails()
        {
            var countries = await _dbContext.Countries
                .AsNoTracking()
                .Include(q => q.Hotels)
                .ToListAsync();

            return countries;
        }

        public async Task<Country> GetCountryWithDetails(int id)
        {
            var country = await _dbContext.Countries
                .AsNoTracking()
                .Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);

            return country;
        }
    }
}
