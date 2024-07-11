namespace HotelListing.MVC.Models.Hotel.Common
{
    public class BaseHotelVM
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}
