using HotelListing.MVC.Models.Country;

namespace HotelListing.MVC.Models.Hotel
{
    public class HotelVM : UpdateHotelVM
    {
        public CountryVM Country { get; set; }
    }
}
