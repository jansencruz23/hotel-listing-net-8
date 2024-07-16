using HotelListing.Application.Contracts.Persistence;
using HotelListing.Application.Models.Pagination;
using HotelListing.Domain.Models;
using HotelListing.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Persistence.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelListingDbContext _dbContext;

        public HotelRepository(HotelListingDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Hotel>> GetAllHotelsWithDetails()
        {
            var hotels = await _dbContext.Hotels
                .AsNoTracking()
                .Include(q => q.Country)
                .ToListAsync();

            return hotels;
        }

        public async Task<IPagedList<Hotel>> GetAllHotelsWithDetails(RequestParams requestParams)
        {
            var hotels = await _dbContext.Hotels
                .AsNoTracking()
                .Include(q => q.Country)
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);

            return hotels;
        }

        public async Task<List<Hotel>> GetHotelsByCountryId(int id)
        {
            var hotels = await _dbContext.Hotels
                .AsNoTracking()
                .Where(q => q.CountryId == id)
                .Include(q => q.Country)
                .ToListAsync();

            return hotels;
        }

        public async Task<Hotel> GetHotelWithDetails(int id)
        {
            var hotel = await _dbContext.Hotels
                .AsNoTracking()
                .Include(q => q.Country)
                .FirstOrDefaultAsync(q => q.Id == id);

            return hotel;
        }
    }
}
