using System.ComponentModel.DataAnnotations;

namespace HotelListing.MVC.Models.Country.Common
{
    public abstract class BaseCountryVM
    {
        [Required]
        [MaxLength(50, ErrorMessage = "{0} must not exceed {1} characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code Name")]
        [MaxLength(3, ErrorMessage = "{0} must not exceed {1} characters.")]
        public string CodeName { get; set; }
    }
}
