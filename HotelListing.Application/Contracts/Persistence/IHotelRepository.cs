using HotelListing.Application.Contracts.Persistence.Common;
using HotelListing.Application.Models.Pagination;
using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Application.Contracts.Persistence
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<List<Hotel>> GetAllHotelsWithDetails();
        Task<IPagedList<Hotel>> GetAllHotelsWithDetails(RequestParams requestParams);
        Task<Hotel> GetHotelWithDetails(int id);
        Task<List<Hotel>> GetHotelsByCountryId(int id);
    }
}
