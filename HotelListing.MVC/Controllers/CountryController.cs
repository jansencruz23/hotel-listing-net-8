using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Country;
using HotelListing.MVC.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace HotelListing.MVC.Controllers
{
    public class CountryController(
        ICountryService _countryService
        ) : Controller
    {
        // GET: CountryController
        public async Task<ActionResult> Index()
        {
            var model = await _countryService.GetAllCountries();
            return View(model);
        }

        // GET: CountryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _countryService.GetCountry(id);
            return View(model);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCountryVM country)
        {
            if (ModelState.IsValid)
            {
                var response = await _countryService.CreateCountry(country);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", string.Join("\n", response.Errors));
            }

            return View(country);
        }

        // GET: CountryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _countryService.GetUpdateCountry(id);
            return View(model);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateCountryVM country)
        {
            if (ModelState.IsValid)
            {
                var response = await _countryService.UpdateCountry(id, country);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", string.Join("\n", response.Errors));
            }

            return View(country);
        }

        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _countryService.DeleteCountry(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", string.Join("\n", response.Errors));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return BadRequest();
        }
    }
}
