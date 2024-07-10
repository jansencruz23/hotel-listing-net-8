using HotelListing.Application.Contracts.Persistence.Common;
using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Contracts.Persistence
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<List<Hotel>> GetAllHotelsWithDetails();
        Task<Hotel> GetHotelWithDetails(int id);
    }
}
