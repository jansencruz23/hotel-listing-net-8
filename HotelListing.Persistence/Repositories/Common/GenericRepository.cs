using HotelListing.Application.Contracts.Persistence.Common;
using HotelListing.Application.Models.Pagination;
using HotelListing.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Persistence.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseDomainEntity
    {
        private readonly HotelListingDbContext _dbContext;

        public GenericRepository(HotelListingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            var result = await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            _dbContext.Remove(entity);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<int> TotalCount()
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .CountAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).OriginalValues["Version"] = entity.Version;
            _dbContext.Update(entity);
        }
    }
}
