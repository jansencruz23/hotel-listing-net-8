using HotelListing.MVC.Models.Hotel;

namespace HotelListing.MVC.Models.Country
{
    public class CountryVM : UpdateCountryVM
    {
        public List<HotelVM> Hotels { get; set; }
    }
}
