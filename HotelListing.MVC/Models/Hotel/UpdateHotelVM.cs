using HotelListing.MVC.Models.Hotel.Common;

namespace HotelListing.MVC.Models.Hotel
{
    public class UpdateHotelVM : CreateHotelVM
    {
        public int Id { get; set; }
        public Guid Version { get; set; }
    }
}
