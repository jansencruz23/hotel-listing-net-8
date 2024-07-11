using System.ComponentModel.DataAnnotations;

namespace HotelListing.MVC.Models.Hotel.Common
{
    public class BaseHotelVM
    {
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must not exceed {1} characters.")]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "{0} should be between {1} and {2}")]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
