using HotelListing.MVC.Models.Country;

namespace HotelListing.MVC.Models.Hotel
{
    public class HotelVM : CreateHotelVM
    {
        public int Id { get; set; }
        public CountryVM Country { get; set; }
    }
}
