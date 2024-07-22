using HotelListing.MVC.Models.Country.Common;

namespace HotelListing.MVC.Models.Country
{
    public class UpdateCountryVM : BaseCountryVM
    {
        public int Id { get; set; }
        public Guid Version { get; set; }
    }
}
