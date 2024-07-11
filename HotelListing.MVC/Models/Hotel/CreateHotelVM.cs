using HotelListing.MVC.Models.Hotel.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelListing.MVC.Models.Hotel
{
    public class CreateHotelVM : BaseHotelVM
    {
        public SelectList? Countries { get; set; }
    }
}
