using HotelListing.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Abstractions.Identity
{
    public interface IUserService
    {
        public string UserId { get; }
        Task<User> GetUser(string id);
        Task<List<User>> GetUsers();
    }
}
