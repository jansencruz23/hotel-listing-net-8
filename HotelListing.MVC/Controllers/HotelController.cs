using HotelListing.MVC.Contracts;
using HotelListing.MVC.Models.Hotel;
using HotelListing.MVC.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelListing.MVC.Controllers
{
    public class HotelController(
        IHotelService _hotelService,
        ICountryService _countryService
        ) : Controller
    {
        // GET: HotelController
        public async Task<ActionResult> Index(RequestParams requestParams)
        {
            var model = await _hotelService.GetAllHotels(requestParams);
            return View(model);
        }

        // GET: HotelController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _hotelService.GetHotel(id);
            return View(model);
        }

        // GET: HotelController/Create
        public async Task<ActionResult> Create()
        {
            var countries = await _countryService.GetAllCountries();
            var countryItems = new SelectList(countries, "Id", "Name");
            var model = new CreateHotelVM { Countries = countryItems };

            return View(model);
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateHotelVM hotel)
        {
            if (ModelState.IsValid)
            {
                var response = await _hotelService.CreateHotel(hotel);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", string.Join("\n", response.Errors));
            }

            var countries = await _countryService.GetAllCountries();
            var countryItems = new SelectList(countries, "Id", "Name");
            var model = new CreateHotelVM { Countries = countryItems };

            return View(model);
        }

        // GET: HotelController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _hotelService.GetUpdateHotel(id);
            var countries = await _countryService.GetAllCountries();
            var countryItems = new SelectList(countries, "Id", "Name");
            model.Countries = countryItems;

            return View(model);
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateHotelVM hotel)
        {
            if (ModelState.IsValid)
            {
                var response = await _hotelService.UpdateHotel(id, hotel);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", string.Join("\n", response.Errors));
            }

            var countries = await _countryService.GetAllCountries();
            var countryItems = new SelectList(countries, "Id", "Name");
            var model = new UpdateHotelVM { Countries = countryItems };

            return View(model);
        }

        // POST: HotelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _hotelService.DeleteHotel(id);
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
