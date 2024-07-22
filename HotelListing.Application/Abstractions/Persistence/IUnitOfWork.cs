using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository HotelRepository { get; }
        ICountryRepository CountryRepository { get; }
        Task SaveAsync();
    }
}
