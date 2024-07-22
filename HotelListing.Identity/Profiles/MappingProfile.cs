using AutoMapper;
using HotelListing.Application.Models.Identity;
using HotelListing.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Identity.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, User>().ReverseMap();
        }
    }
}
