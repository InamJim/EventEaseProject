using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using EventEase.Services.Interfaces;

namespace EventEase.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IEventService _eventService;
        private readonly IVenueService _venueService;

        public BookingController(
            IBookingService bookingService,
            IEventService eventService,
            IVenueService venueService)
        {
            _bookingService = bookingService;
            _eventService = eventService;
            _venueService = venueService;
        }

        public IActionResult Index()
        {
            ViewBag.Events = _eventService.GetAll();
            ViewBag.Venues = _venueService.GetAll();

            var bookings = _bookingService.GetAll();
            return View(bookings);
        }

        public IActionResult Create()
        {
            ViewBag.Events = _eventService.GetAll();
            ViewBag.Venues = _venueService.GetAll();

            return View();
        }

        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction(nameof(Index));
            }

            var results = _bookingService.Search(keyword);
            return View("Index", results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (!ModelState.IsValid)
                return View(booking);

            bool success = _bookingService.Create(booking);

            if (!success)
            {
                TempData["Error"] = "❌ Double booking detected for this venue on this date.";

                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _bookingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}