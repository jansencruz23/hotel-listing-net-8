using HotelListing.Application.Contracts.Persistence.Common;
using HotelListing.Application.DTOs.Country;
using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Contracts.Persistence
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<List<Country>> GetAllCountriesWithDetails();
        Task<Country> GetCountryWithDetails(int id);
    }
}
