using HotelListing.MVC.Models.Country.Common;
using HotelListing.MVC.Models.Hotel;

namespace HotelListing.MVC.Models.Country
{
    public class CountryVM : BaseCountryVM
    {
        public int Id { get; set; }
        public List<HotelVM> Hotels { get; set; }
    }
}
