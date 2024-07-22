using AutoMapper;
using HotelListing.Application.Abstractions.Identity;
using HotelListing.Application.Constants;
using HotelListing.Application.Models.Identity;
using HotelListing.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Identity.Services
{
    public class UserService (
        UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        IMapper _mapper
        ) : IUserService
    {
        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(CustomClaimTypes.Uid).Value;

        public async Task<User> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<User>(user);
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync(CustomRoles.User);
            return _mapper.Map<List<User>>(users);
        }
    }
}
