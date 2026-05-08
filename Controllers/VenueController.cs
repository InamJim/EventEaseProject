using EventEase.Models;
using EventEase.Services.Blob;
using EventEase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly IFileUploadService _fileUpload;

        public VenueController(IVenueService venueService, IFileUploadService fileUpload)
        {
            _venueService = venueService;
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            var venues = _venueService.GetAll();
            return View(venues);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Search(string keyword)
        {
            var result = _venueService.Search(keyword);
            return View("Index", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
                return View(venue);

            if (imageFile != null)
            {
                venue.ImageUrl = await _fileUpload.UploadFileAsync(imageFile);
            }

            _venueService.Create(venue);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var venue = _venueService.GetById(id);
            return View(venue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Venue venue)
        {
            _venueService.Update(venue);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _venueService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}