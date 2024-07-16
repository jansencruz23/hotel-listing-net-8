using HotelListing.Application.Models.Pagination;
using HotelListing.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Application.Contracts.Persistence.Common
{
    public interface IGenericRepository<T>
        where T : BaseDomainEntity
    {
        Task<List<T>> GetAll();
        Task<IPagedList<T>> GetAll(RequestParams requestParams);
        Task<T> Get(int id);
        Task<T> Add(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<int> TotalCount();
    }
}
