using Microsoft.AspNetCore.Mvc;
using EventEase.Services.Interfaces;

namespace EventEase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly IEventService _eventService;
        private readonly IBookingService _bookingService;

        public HomeController(
            IVenueService venueService,
            IEventService eventService,
            IBookingService bookingService)
        {
            _venueService = venueService;
            _eventService = eventService;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalVenues = _venueService.GetAll().Count();
            ViewBag.TotalEvents = _eventService.GetAll().Count();
            ViewBag.TotalBookings = _bookingService.GetAll().Count();

            return View();
        }
    }
}