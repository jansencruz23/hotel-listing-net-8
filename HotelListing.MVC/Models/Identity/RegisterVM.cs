using System.ComponentModel.DataAnnotations;

namespace HotelListing.MVC.Models.Identity
{
    public class RegisterVM
    {
        [Required]
        [MinLength(6, ErrorMessage = "{0} must be at least {1} characters.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "{0} must be at least {1} characters.")]
        public string Password { get; set; }

        [Required, Display(Name = "Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "{0} must be at least {1} characters.")]
        public string ConfirmPassword { get; set; }
    }
}
