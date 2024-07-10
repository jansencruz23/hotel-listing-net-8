using HotelListing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelListingDbContext _dbContext;
        private IHotelRepository _hotelRepository;
        private ICountryRepository _countryRepository;

        public UnitOfWork(HotelListingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IHotelRepository HotelRepository =>
            _hotelRepository ??= new HotelRepository(_dbContext);

        public ICountryRepository CountryRepository =>
            _countryRepository ??= new CountryRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
