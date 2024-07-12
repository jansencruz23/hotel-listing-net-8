using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.MVC.Controllers
{
    public class UserController(
        IClientAuthenticationService _authService
        ) : Controller
    {
        public IActionResult Login(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.Login(login);
                
                if (response.Success)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Log in attempt failed. Please try again.");
            return View(login);
        }

        public IActionResult Register(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registration, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                returnUrl = Url.Content("~/");
                var response = await _authService.Register(registration);
                
                if (response.Success)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Registration attempt failed. Please check your details and try again.");
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction(nameof(Login));
        }
    }
}
