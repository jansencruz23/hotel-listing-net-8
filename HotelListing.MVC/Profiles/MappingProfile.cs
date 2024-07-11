using AutoMapper;
using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Services.Base;

namespace HotelListing.MVC.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CountryDto, CountryVM>().ReverseMap();
            CreateMap<CreateCountryDto, CreateCountryVM>().ReverseMap();
            CreateMap<UpdateCountryDto, UpdateCountryVM>().ReverseMap();
            CreateMap<CountryDto, UpdateCountryVM>().ReverseMap();

            CreateMap<HotelDto, HotelVM>().ReverseMap();
            CreateMap<CreateHotelDto, CreateHotelVM>().ReverseMap();
            CreateMap<UpdateHotelDto, UpdateHotelVM>().ReverseMap();
            CreateMap<HotelDto, UpdateHotelVM>().ReverseMap();
        }
    }
}
